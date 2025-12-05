using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Scs.Infrastructure.Persistence;
using System.Reflection;

namespace Scs.DbMigration.PostgreSQL
{ 
    public static class DependencyInjection
    {

        /// <summary>
        /// Registers the PostgresScsDbContext with the service collection.
        /// </summary>
        /// <param name="option">The infrastructure option wrapper containing services and configuration.</param>
        /// <param name="tempConfiguration">Optional configuration override, useful for design-time in some legacy setups.</param>
        /// <returns>The service collection for chaining.</returns>
        public static IServiceCollection UsePostgreSQL(this InfrastructureOption option, IConfiguration tempConfiguration = null)
        {
            var services = option.Services;
            // Use tempConfiguration if provided, otherwise use the configuration from the option wrapper
            var configuration = tempConfiguration ?? option.Configuration;

            // Ensure your connection string key is correct: 
            // Previous code used "Connection.PostgreSQL", but the Factory used "DefaultConnection". 
            // I'll stick to "Connection.PostgreSQL" as it was in your DependencyInjection code.
            string conString = configuration.GetConnectionString("DefaultConnection");

            // Recommended Npgsql setting for compatibility with older DateTime values in PostgreSQL
            // It's good practice to set this once during application initialization.
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddDbContext<PostgresScsDbContext>((scs, options) =>
            {
                options.UseNpgsql(
                    conString,
                    npgsqlOptionsAction: opt =>
                    {
                        // Use the assembly where the migrations reside (Scs.DbMigration.PostgreSQL)
                        opt.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                        // Specify the schema and table name for the EF Core migrations history
                        opt.MigrationsHistoryTable("__EFMigrationsHistory", "adm");
                    }
                );
            });

            // Register the concrete context against the base/abstract types for DI consumption
            // Assuming 'ScsDbContext' is the abstract base class or interface used in your application
            services.AddScoped<ScsDbContext>(provider => provider.GetRequiredService<PostgresScsDbContext>());
            services.AddScoped<DbContext>(provider => provider.GetRequiredService<PostgresScsDbContext>());

            return services;
        }
    }
}