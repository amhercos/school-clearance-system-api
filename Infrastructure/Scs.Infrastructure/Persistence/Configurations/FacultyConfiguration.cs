using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.EmployeeId)
                .IsRequired()
                .HasMaxLength(20);

           
            builder.HasIndex(f => f.EmployeeId).IsUnique();
            builder.HasIndex(f => f.DepartmentId);


        }
    }
}
