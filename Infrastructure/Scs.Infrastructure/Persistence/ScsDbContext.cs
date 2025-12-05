using Microsoft.EntityFrameworkCore;
using Scs.Application.Services;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scs.Infrastructure.Persistence
{
    public class ScsDbContext : DbContext, IScsDbContext
    {
        public ScsDbContext(DbContextOptions options) : base(options) { }
        //public DbSet<User> Users => Set<User>();

        public DbSet<Student> Students => Set<Student>();

        public DbSet<Faculty> Faculties => Set<Faculty>();

        public DbSet<ClearanceForm> ClearanceForms => Set<ClearanceForm>();

        public DbSet<ClearanceSignature> ClearanceSignatures => Set<ClearanceSignature>();

        public DbSet<Department> Departments => Set<Department>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
