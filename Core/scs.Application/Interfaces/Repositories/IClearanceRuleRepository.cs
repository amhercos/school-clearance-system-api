using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IClearanceRuleRepository :
        ICommandRepository<ClearanceRule>, 
        IQueryRepository<ClearanceRule>
    {

    }
}
