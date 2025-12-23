using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces;
using Scs.Domain.Entities;
using Scs.Domain.Entities.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace Scs.Infrastructure.Persistence
{
    public class ScsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, IScsDbContext
    {
        public ScsDbContext(DbContextOptions options) : base(options) { }
     

        public DbSet<Student> Students => Set<Student>();

        public DbSet<Faculty> Faculties => Set<Faculty>();

        public DbSet<ClearanceForm> ClearanceForms => Set<ClearanceForm>();

        public DbSet<ClearanceSignature> ClearanceSignatures => Set<ClearanceSignature>();

        public DbSet<Department> Departments => Set<Department>();

        public DbSet<ClearanceRule> ClearanceRules => Set<ClearanceRule>();

        public DbSet<ClearanceSignatory> ClearanceSignatories => Set<ClearanceSignatory>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            // ASPNET IDENTITY TABLES 
            // ApplicationUser => Student
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.StudentProfile)
                .WithOne(s => s.ApplicationUser)
                .HasForeignKey<Student>(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);
            // ApplicationUser => Faculty
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.FacultyProfile)
                .WithOne(f => f.ApplicationUser)
                .HasForeignKey<Faculty>(f => f.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Check if entity implements ISoftDelete
                if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, nameof(ISoftDelete.IsDeleted));
                    var falseConstant = Expression.Constant(false);
                    var comparison = Expression.Equal(property, falseConstant);

                    var lambda = Expression.Lambda(comparison, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }
    }
}
