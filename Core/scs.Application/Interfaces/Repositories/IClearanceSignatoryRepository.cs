using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application.Interfaces.Repositories
{
    public interface IClearanceSignatoryRepository:
        ICommandRepository<ClearanceSignatory>,
        IQueryRepository<ClearanceSignatory>
    {
        Task<ClearanceSignatory?> GetByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);
        Task<Guid> AssignOrUpdateSignatoryAsync(Guid departmentId, Guid facultyId, CancellationToken cancellationToken = default);
    }
}
