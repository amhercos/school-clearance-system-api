using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories
{
    public interface ICommandRepository <T> where T : class
    {
        
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        void Update(T entity);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
