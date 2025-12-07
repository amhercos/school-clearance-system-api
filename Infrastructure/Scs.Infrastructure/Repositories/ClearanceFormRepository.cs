using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;

namespace Scs.Infrastructure.Repositories
{
    public class ClearanceFormRepository : Repository<ClearanceForm>, IClearanceFormRepository
    {

        public ClearanceFormRepository(ScsDbContext context) : base(context)
        {
        }


        public override async Task<IReadOnlyList<ClearanceForm>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var forms = await _dbContext.ClearanceForms
                        .AsNoTracking()
                        .Include(cf => cf.ClearanceSignatures)
                        .ToListAsync(cancellationToken);
            return forms;
        }

        public override async Task<ClearanceForm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ClearanceForms
                     .Include(cf => cf.ClearanceSignatures)
                         .ThenInclude(cs => cs.Department)
                     .Include(cf => cf.ClearanceSignatures)
                         .ThenInclude(cs => cs.SignedByFaculty)
                             .ThenInclude(f => f.ApplicationUser)
                     .FirstOrDefaultAsync(cf => cf.Id == id, cancellationToken);
        }
    }
}