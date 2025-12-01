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
        DbSet<FacultySignatory> FacultySignatories { get; }
        DbSet<ClearanceForm> ClearanceForms { get; }
        DbSet<ClearanceSignature> ClearanceSignatures { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
