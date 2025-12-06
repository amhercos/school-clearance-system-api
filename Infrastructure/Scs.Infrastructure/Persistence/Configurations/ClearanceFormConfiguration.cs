using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class ClearanceFormConfiguration : IEntityTypeConfiguration<ClearanceForm>
    {
        public void Configure(EntityTypeBuilder<ClearanceForm> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.ClearanceSignatures)
            .WithOne(s => s.ClearanceForm)
            .HasForeignKey(s => s.ClearanceFormId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Student)
                .WithMany(c=>c.ClearanceForms)
                .HasForeignKey(c=>c.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
