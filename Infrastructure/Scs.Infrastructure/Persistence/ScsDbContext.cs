using Microsoft.EntityFrameworkCore;
using Scs.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using Scs.Application.Interfaces;

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
        }
    }
}
