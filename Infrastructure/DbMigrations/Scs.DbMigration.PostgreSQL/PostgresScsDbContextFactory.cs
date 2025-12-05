using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Scs.DbMigration.PostgreSQL
{
    /// <summary>
    /// Design-time factory for PostgresScsDbContext. 
    /// This allows the 'dotnet ef' tools to create an instance of the DbContext
    /// for migration creation and database updates.
    /// </summary>
    public class PostgresScsDbContextFactory : IDesignTimeDbContextFactory<PostgresScsDbContext>
    {
        public PostgresScsDbContext CreateDbContext(string[] args)
        {
            // 1. Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // Ensure appsettings.json is available in the migration project's output directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // 2. Get the connection string
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    "The connection string 'DefaultConnection' was not found in 'appsettings.json'.");
            }

            // 3. Configure DbContext options
            var optionsBuilder = new DbContextOptionsBuilder<PostgresScsDbContext>();

            // Use Npgsql for PostgreSQL configuration
            optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
            {
                // Optionally configure common settings like migrations assembly
                // The assembly name should be the name of the project where this factory resides
                sqlOptions.MigrationsAssembly(typeof(PostgresScsDbContextFactory).Assembly.FullName);
            });

            // 4. Create and return the DbContext instance
            return new PostgresScsDbContext(optionsBuilder.Options);
        }
    }
}