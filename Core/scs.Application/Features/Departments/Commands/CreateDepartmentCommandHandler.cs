using MediatR;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities; // Assuming your Department entity is here
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Scs.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, Guid>
    {
        private readonly IDepartmentRepository _repository;

        public CreateDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            bool isCodeUnique = await _repository.IsDepartmentCodeUniqueAsync(request.DepartmentCode, cancellationToken);

            if (!isCodeUnique)
            {
                throw new ArgumentException($"The department code '{request.DepartmentCode}' already exists.", nameof(request.DepartmentCode));
            }

            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DepartmentCode = request.DepartmentCode,
            };

         
            await _repository.AddAsync(department, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            return department.Id;
        }
    }
}