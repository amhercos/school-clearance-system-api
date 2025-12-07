using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Scs.Application.Interfaces;
using Scs.Application.Interfaces.Repositories;
using Scs.Domain.Entities;
using Scs.Infrastructure.Persistence;
using Scs.Infrastructure.Repositories;
using Scs.Infrastructure.Services;
using System.Text;

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

            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
               .AddEntityFrameworkStores<ScsDbContext>()
                .AddDefaultTokenProviders();

            // JWT 
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                string secretString = configuration["JwtSettings:Key"] ?? throw new ArgumentNullException("JwtSettings:Key");
                if (string.IsNullOrEmpty(secretString))
                {
                    throw new InvalidOperationException("JwtSettings:Key is empty or null after configuration load.");
                }
                var key = Encoding.UTF8.GetBytes(secretString);

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });


            services.AddScoped<IClearanceFormRepository, ClearanceFormRepository>();
            services.AddScoped<IDataSeeder, DataSeeder>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<IScsDbContext, ScsDbContext>();

            return services;
        }
    }
}