using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Scs.Application.Interfaces.Repositories;
using Scs.Infrastructure.Repositories;
using Scs.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace Scs.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        { 
            services.AddDbContext<ScsDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // 2. Repositories
            services.AddScoped<IClearanceFormRepository, ClearanceFormRepository>();

            return services;
        }
    }
}