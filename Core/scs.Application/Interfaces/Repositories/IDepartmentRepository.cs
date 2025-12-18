using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : 
        ICommandRepository<Department>, 
        IQueryRepository<Department>
    {
        Task<bool> IsDepartmentCodeUniqueAsync(string code, CancellationToken cancellationToken = default);
    }
}

