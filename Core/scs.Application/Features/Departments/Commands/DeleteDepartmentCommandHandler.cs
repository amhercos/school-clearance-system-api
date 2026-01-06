using MediatR;
using Scs.Application.Exceptions;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;


namespace Scs.Application.Features.Departments.Commands
{
    public class DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository) : IRequestHandler<DeleteDepartmentCommand>
    {
        public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var department = await departmentRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Department), request.Id);
            await departmentRepository.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
