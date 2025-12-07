using MediatR;

namespace Scs.Application.Features.ClearanceRules.Commands
{
    public class CreateClearanceRuleCommand : IRequest<Guid>
    {
        public Guid RequiredDepartmentId { get; set; }
        public Guid? AppliesToStudentDepartmentId { get; set; }
    }
    
}
