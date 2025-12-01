using Microsoft.EntityFrameworkCore;
using Scs.Application.Services;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence
{
    public class ScsDbContext : DbContext, IScsDbContext
    {
        public DbSet<User> Users => Set<User>();

        public DbSet<Student> Students => Set<Student>();

        public DbSet<FacultySignatory> FacultySignatories => Set<FacultySignatory>();

        public DbSet<ClearanceForm> ClearanceForms => Set<ClearanceForm>();

        public DbSet<ClearanceSignature> ClearanceSignatures => Set<ClearanceSignature>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
                

        }
    }
}
