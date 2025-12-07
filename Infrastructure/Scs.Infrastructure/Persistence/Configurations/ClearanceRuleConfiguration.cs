using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scs.Domain.Entities;

namespace Scs.Infrastructure.Persistence.Configurations
{
    public class ClearanceRuleConfiguration : IEntityTypeConfiguration<ClearanceRule>
    {
        public void Configure(EntityTypeBuilder<ClearanceRule> builder)
        {
            builder.HasKey(cr => cr.Id);

            builder.HasOne(cr => cr.RequiredDepartment)
                .WithMany()
                .HasForeignKey(cr => cr.RequiredDepartmentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(cr => cr.StudentDepartment)
                .WithMany()
                .HasForeignKey(cr => cr.StudentDepartmentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(cr => new { cr.RequiredDepartmentId, cr.StudentDepartmentId })
                .IsUnique();
        }
    }
}