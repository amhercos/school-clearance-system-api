using Microsoft.EntityFrameworkCore;
using Scs.Application.DTOs;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Application.Interfaces.Repositories.Common;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scs.Infrastructure.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(ScsDbContext dbContext) : base(dbContext) { }

        public async Task<bool> IsDepartmentCodeUniqueAsync(string code, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                               .AnyAsync(d => d.DepartmentCode == code, cancellationToken);
        }

    }
}
