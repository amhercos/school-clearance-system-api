using MediatR;

namespace Scs.Application.Features.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string DepartmentCode { get; set; }
    }
}
