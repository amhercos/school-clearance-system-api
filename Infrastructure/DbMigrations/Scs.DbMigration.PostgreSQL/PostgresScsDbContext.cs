using Microsoft.EntityFrameworkCore;
using Npgsql;
using Scs.Infrastructure.Persistence;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Scs.DbMigration.PostgreSQL
{
    public class PostgresScsDbContext : ScsDbContext
    {

        public PostgresScsDbContext(DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbEx) when (dbEx.InnerException is PostgresException pg && pg.SqlState == PostgresErrorCodes.UniqueViolation)
            {
                var message = pg.ConstraintName switch
                {
                    "IX_UserProfiles_Email" => "Account email already exists.",
                    "IX_UserProfiles_MobilePhone" => "Account Mobile Number already exists.",
                    "ux_facility_code" => "Facility code already exists.",
                    "IX_UserCredentials_Username" => "Account username already exists.",
                    _ => "Duplicate value violates a unique constraint."
                };

                
                throw new InvalidOperationException($"A unique constraint violation occurred: {message} (Constraint: {pg.ConstraintName})", dbEx);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;
                if (clrType == null) continue;

                foreach (var property in clrType.GetProperties())
                {
                    var jsonPropertyNameAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
                    if (jsonPropertyNameAttribute != null)
                    {
                        modelBuilder.Entity(clrType)
                            .Property(property.Name)
                            .HasColumnName(jsonPropertyNameAttribute.Name);
                    }
                }
            }
        }
    }
}