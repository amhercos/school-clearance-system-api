using MediatR;
using Scs.Application.DTOs;

namespace Scs.Application.Features.Departments.Queries
{
    public class GetDepartmentByIdQuery : IRequest<DepartmentDto>
    {
        public Guid Id { get; set; }
    }
}
