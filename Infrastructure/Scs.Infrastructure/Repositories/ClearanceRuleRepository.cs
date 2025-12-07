using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace Scs.Infrastructure.Repositories
{
    public class ClearanceRuleRepository : Repository<ClearanceRule>, IClearanceRuleRepository
    {
        public ClearanceRuleRepository(ScsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
