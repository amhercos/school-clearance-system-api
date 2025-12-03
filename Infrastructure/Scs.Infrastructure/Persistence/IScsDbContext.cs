using Microsoft.EntityFrameworkCore;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence
{
    public interface IScsDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Student> Students { get; }
        DbSet<Faculty> Faculties { get; }
        DbSet<ClearanceForm> ClearanceForms { get; }
        DbSet<ClearanceSignature> ClearanceSignatures { get; }
        DbSet<Department> Departments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
