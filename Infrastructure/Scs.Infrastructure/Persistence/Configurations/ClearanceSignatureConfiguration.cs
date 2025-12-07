using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class ClearanceSignatureConfiguration : IEntityTypeConfiguration<ClearanceSignature>
    {
        public void Configure(EntityTypeBuilder<ClearanceSignature> builder)
        {
            builder.HasKey(cs => cs.Id);
            builder.HasIndex(cs => new { cs.ClearanceFormId, cs.DepartmentId })
            .IsUnique();

            builder.HasOne(cs => cs.Department)
                .WithMany()
                .HasForeignKey(cs => cs.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.ClearanceForm)
                .WithMany(c => c.ClearanceSignatures)
                .HasForeignKey(cs => cs.ClearanceFormId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cs => cs.SignedByFaculty)
                .WithMany()
                .HasForeignKey(cs => cs.SignedByFacultyId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
