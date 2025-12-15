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

            // Relationship 1: One-to-Many with ClearanceForms
            builder.HasMany(s => s.ClearanceForms)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .OnDelete(DeleteBehavior.Cascade); // If student is deleted, forms are deleted.

            // 🔑 NECESSARY ADDITION: One-to-One with ApplicationUser (Identity User)
            builder.HasOne(s => s.ApplicationUser)
                .WithOne()
                .HasForeignKey<Student>(s => s.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔑 NECESSARY ADDITION: Many-to-One with Department (Student's Major)
            builder.HasOne(s => s.Department)
                .WithMany() // Department has many students
                .HasForeignKey(s => s.DepartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
