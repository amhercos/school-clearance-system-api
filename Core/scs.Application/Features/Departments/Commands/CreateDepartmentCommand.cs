using MediatR;
using Scs.Application.DTOs;

namespace Scs.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<DepartmentDto>
    {
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
    }
}
