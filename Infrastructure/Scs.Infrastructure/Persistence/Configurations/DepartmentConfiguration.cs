using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.DepartmentCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(d => d.DepartmentCode)
                .IsUnique();

            builder.HasMany(d => d.Faculties)
                .WithOne(f => f.Department)
                .HasForeignKey(f => f.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
