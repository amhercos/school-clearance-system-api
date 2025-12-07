using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories.Common
{
    public interface ICommandRepository <T> where T : class
    {
        
        Task AddAsync(T entity, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
