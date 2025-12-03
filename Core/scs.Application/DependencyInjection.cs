using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scs.Application
{
    public static class DependencyInjection
    { 
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            //services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
