using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;

namespace Scs.Infrastructure.Repositories.Common
{
    public class Repository<T> : ICommandRepository<T>, IQueryRepository<T> where T : BaseEntity
    {
        protected readonly ScsDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(ScsDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _dbSet
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync(cancellationToken);
        }

        public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FindAsync(new object[] { id }, cancellationToken);
        }
    }
}