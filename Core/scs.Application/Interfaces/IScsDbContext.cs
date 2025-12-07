using Microsoft.EntityFrameworkCore;
using Scs.Domain.Entities;

namespace Scs.Application.Interfaces
{
    public interface IScsDbContext
    {
        DbSet<Student> Students { get; }
        DbSet<Faculty> Faculties { get; }
        DbSet<ClearanceForm> ClearanceForms { get; }
        DbSet<ClearanceSignature> ClearanceSignatures { get; }
        DbSet<Department> Departments { get; }

        DbSet<ClearanceRule> ClearanceRules { get; }
        DbSet<ClearanceSignatory> ClearanceSignatories { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
