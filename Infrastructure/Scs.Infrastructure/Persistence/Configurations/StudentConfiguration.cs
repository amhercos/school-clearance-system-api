using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.StudentNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(s => s.StudentNumber).IsUnique();
            builder.HasIndex(s => s.DepartmentId);

            // Relationship 1: One-to-Many with ClearanceForms
            builder.HasMany(s => s.ClearanceForms)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade); 

        }
    }
}
