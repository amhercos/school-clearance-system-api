using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using System.Security.Claims;

namespace Scs.Application.Features.ClearanceForms.Commands
{
    public class CreateClearanceFormCommandHandler : IRequestHandler<CreateClearanceFormCommand, Guid>
    {
        private readonly IClearanceFormRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IScsDbContext _dbContext;

        public CreateClearanceFormCommandHandler(
            IClearanceFormRepository repository,
            IHttpContextAccessor httpContextAccessor, 
            IScsDbContext dbContext)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateClearanceFormCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out Guid applicationUserId))
            {
                throw new UnauthorizedAccessException("User identity could not be retrieved from the authentication token.");
            }

            var studentId = await _dbContext.Students
                .Where(s => s.ApplicationUserId == applicationUserId)
                .Select(s => s.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (studentId == Guid.Empty)
            {
                throw new InvalidOperationException("Authenticated user does not have a linked Student profile required to create a form.");
            }

            
            var entity = ClearanceForm.Create(
                studentId,
                request.AcademicYear,
                request.Semester,
                request.Program,
                request.YearLevel
            );

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}