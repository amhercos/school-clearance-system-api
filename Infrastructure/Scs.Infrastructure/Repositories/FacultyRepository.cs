using Microsoft.EntityFrameworkCore;
using Scs.Application.DTOs;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Repositories
{
    public class FacultyRepository : Repository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(ScsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<FacultyDto>> GetAllFacultiesWithDetailsAsync(CancellationToken cancellationToken = default)
        {
            //return await _dbSet
            //.AsNoTracking()
            //.Include(f => f.ApplicationUser)
            //.Include(f => f.Department)
            //.ToListAsync(cancellationToken);

            return await _dbSet
                .AsNoTracking()
                .Select(f => new FacultyDto
                {
                    Id = f.Id,
                    EmployeeId = f.EmployeeId,
                    FullName = f.ApplicationUser != null
                        ? f.ApplicationUser.FirstName + " " + f.ApplicationUser.LastName
                        : "No User Linked",
                    Email = f.ApplicationUser.Email ?? "N/A",
                    DepartmentName = f.Department.Name ?? "Unassigned",
                    DepartmentId = f.DepartmentId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
