using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;

namespace Scs.Infrastructure.Repositories
{
    public class ClearanceFormRepository : IClearanceFormRepository
    {
        private readonly ScsDbContext _context;
        public ClearanceFormRepository(ScsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ClearanceForm entity, CancellationToken cancellationToken = default)
        {
            await _context.ClearanceForms.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
           await _context.ClearanceForms
                        .Where(cf => cf.Id == id)
                        .ExecuteDeleteAsync(cancellationToken);
        }  
        
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
           await _context.SaveChangesAsync(cancellationToken);
        }

        public void Update(ClearanceForm entity)
        {
            _context.ClearanceForms.Update(entity);
        }


        // Read Operations
        public async Task<IReadOnlyList<ClearanceForm>> GetAllAsync(CancellationToken cancellationToken = default)
        {
             var forms = await _context.ClearanceForms
                        .AsNoTracking()
                        .Include(cf => cf.ClearanceSignatures)
                        .ToListAsync(cancellationToken);
            return forms;
        }

        public async Task<ClearanceForm?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
          return await _context.ClearanceForms
                        .Include(cf => cf.ClearanceSignatures)
                            .ThenInclude(cs => cs.Department)
                        .Include(cf=> cf.ClearanceSignatures)
                            .ThenInclude(cs => cs.SignedByFaculty)
                        .FirstOrDefaultAsync(cf => cf.Id == id, cancellationToken);
        }

   
    }
}
