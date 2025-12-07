using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories.Common
{
    public interface IQueryRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
