using MediatR;
using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scs.Application.Features.ClearanceSignatories.Commands
{
    public class AssignSignatoryCommandHandler : IRequestHandler<AssignSignatoryCommand, Guid>
    {
        private readonly IScsDbContext _dbContext;
        private readonly IClearanceSignatoryRepository _signatoryRepository;

        public AssignSignatoryCommandHandler(IScsDbContext dbContext, IClearanceSignatoryRepository signatoryRepository)
        {
            _dbContext = dbContext;
            _signatoryRepository = signatoryRepository;
        }

        public async Task<Guid> Handle(AssignSignatoryCommand request, CancellationToken cancellationToken)
        {
            bool departmentExists = await _dbContext.Departments.AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);
            if (!departmentExists)
            {
                throw new ArgumentException($"Department ID '{request.DepartmentId}' not found.", nameof(request.DepartmentId));
            }

            bool facultyExists = await _dbContext.Faculties.AnyAsync(f => f.Id == request.FacultyId, cancellationToken);
            if (!facultyExists)
            {
                throw new ArgumentException($"Faculty ID '{request.FacultyId}' not found.", nameof(request.FacultyId));
            }

            return await _signatoryRepository.AssignOrUpdateSignatoryAsync(
                request.DepartmentId,
                request.FacultyId,
                cancellationToken
            );
        }
    }
}