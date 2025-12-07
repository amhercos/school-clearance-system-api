using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Repositories
{
    public class ClearanceSignatoryRepository : Repository<ClearanceSignatory>, IClearanceSignatoryRepository
    {
        public ClearanceSignatoryRepository(ScsDbContext dbContext) : base(dbContext) { }

        public async Task<Guid> AssignOrUpdateSignatoryAsync(Guid departmentId, Guid facultyId, CancellationToken cancellationToken = default)
        {
            ClearanceSignatory? existingSignatory = await GetByDepartmentIdAsync(departmentId, cancellationToken);

            if (existingSignatory != null)
            {
                existingSignatory.FacultyId = facultyId;
                await SaveChangesAsync(cancellationToken);
                return existingSignatory.Id;
            }
            else
            {
                var newSignatory = new ClearanceSignatory
                {
                    Id = Guid.NewGuid(),
                    DepartmentId = departmentId,
                    FacultyId = facultyId
                };

                await AddAsync(newSignatory, cancellationToken);
                await SaveChangesAsync(cancellationToken);
                return newSignatory.Id;
            }
        }

        public async Task<ClearanceSignatory?> GetByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ClearanceSignatories
                .FirstOrDefaultAsync(cs => cs.DepartmentId == departmentId, cancellationToken);
        }
    }
}
