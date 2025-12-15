using MediatR;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities; 


namespace Scs.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
    {
        private readonly IDepartmentRepository _repository;

        public CreateDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            bool isCodeUnique = await _repository.IsDepartmentCodeUniqueAsync(request.DepartmentCode, cancellationToken);

            if (isCodeUnique)
            {
                throw new InvalidOperationException
                    ($"The department code '{request.DepartmentCode}' already exists.");
            }

            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                DepartmentCode = request.DepartmentCode,
            };


            await _repository.AddAsync(department, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
            var departmentDto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                DepartmentCode = department.DepartmentCode,
      
            };

            return departmentDto;
        }
    }
}