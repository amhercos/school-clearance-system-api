using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class ClearanceSignatoryConfiguration : IEntityTypeConfiguration<ClearanceSignatory>
    {
        public void Configure(EntityTypeBuilder<ClearanceSignatory> builder)
        {
           
            builder.HasKey(cs => cs.Id);
            builder.HasIndex(cs => cs.DepartmentId)
                .IsUnique();
           
            builder.HasOne<Department>()
                .WithOne(c => c.AssignedSignatory)
                .HasForeignKey<ClearanceSignatory>(cs => cs.DepartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Faculty>()
                .WithMany()
                .HasForeignKey(cs => cs.FacultyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}