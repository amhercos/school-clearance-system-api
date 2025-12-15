using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Scs.Application.DTOs;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using System.Security.Claims;

namespace Scs.Application.Features.ClearanceForms.Queries
{
    public class GetClearanceFormByIdQueryHandler : IRequestHandler<GetClearanceFormByIdQuery, ClearanceFormDto>
    {
        private readonly IClearanceFormRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IScsDbContext _dbContext;

        public GetClearanceFormByIdQueryHandler(
            IClearanceFormRepository repository,
            IHttpContextAccessor httpContextAccessor,
            IScsDbContext dbContext)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<ClearanceFormDto> Handle(GetClearanceFormByIdQuery request, CancellationToken cancellationToken)
        {
           
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid applicationUserId))
            {
                throw new UnauthorizedAccessException("User identity could not be retrieved from the authentication token.");
            }

  
            var studentExists = await _dbContext.Students
           .AnyAsync(s => s.Id == applicationUserId, cancellationToken);

            if (!studentExists)
            {
                throw new InvalidOperationException("Authenticated user does not have a linked Student profile required to create a form.");
            }

            var studentId = applicationUserId;

            if (studentId == Guid.Empty)
            {
                throw new InvalidOperationException("Authenticated user is not linked to a student profile.");
            }

       
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                return null;
            }

     
            if (entity.StudentId != studentId)
            {
                throw new UnauthorizedAccessException("You are not authorized to view this clearance form.");
            }

        
            var clearanceFormDto = new ClearanceFormDto
            {
                Id = entity.Id,
                StudentId = entity.StudentId,
                // Assuming OverallStatus is an enum or string field on the entity
                OverallStatus = entity.OverallStatus.ToString(),
                IsCompleted = entity.IsCompleted,

                // --- Mapping Nested Signatures ---
                Signatures = entity.ClearanceSignatures.Select(s =>
                {
                    var faculty = s.SignedByFaculty;
                    // Assumes the Faculty entity has an ApplicationUser navigation property
                    var applicationUser = faculty?.ApplicationUser;

                    string signedByName;

                    // Prioritize ApplicationUser name, then Employee ID, then N/A
                    if (applicationUser != null)
                    {
                        signedByName = $"{applicationUser.FirstName} {applicationUser.LastName}".Trim();
                    }
                    else if (faculty != null)
                    {
                        signedByName = faculty.EmployeeId;
                    }
                    else
                    {
                        signedByName = "N/A";
                    }

                    return new ClearanceSignatureDto
                    {
                        Id = s.Id,
                        DepartmentName = s.Department?.Name ?? "N/A",
                        Status = s.Status.ToString(),
                        Remarks = s.Remarks,
                        ClearanceFormId = s.ClearanceFormId,
                        IsSigned = s.DateActioned.HasValue,
                        SignedByFacultyName = signedByName,
                        SignedDate = s.DateActioned,
                    };
                }).ToList()
            };

            return clearanceFormDto;
        }
    }
}