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
            // --- 1. SECURITY CHECK: Retrieve the Authenticated User's ID ---
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid applicationUserId))
            {
                throw new UnauthorizedAccessException("User identity could not be retrieved from the authentication token.");
            }

            // --- 2. SECURITY CHECK: Get the StudentId linked to the authenticated user ---
            // Assumes IScsDbContext exposes the 'Students' DbSet
            var studentId = await _dbContext.Students
                .Where(s => s.ApplicationUserId == applicationUserId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (studentId == Guid.Empty)
            {
                throw new InvalidOperationException("Authenticated user is not linked to a student profile.");
            }

            // --- 3. FETCH ENTITY ---
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                return null;
            }

            // --- 4. FINAL OWNERSHIP VERIFICATION ---
            if (entity.StudentId != studentId)
            {
                throw new UnauthorizedAccessException("You are not authorized to view this clearance form.");
            }

            // --- 5. MAP TO DTO (Completed Logic) ---
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