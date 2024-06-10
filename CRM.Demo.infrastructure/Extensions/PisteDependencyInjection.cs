using CRM.Demo.App;
using CRM.Demo.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Demo.Infrastructure
{ 
    public static class PisteDependencyInjection
    {
        public static IServiceCollection AddPisteService(this IServiceCollection services
            , IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>            
                options.UseSqlServer(configuration.GetConnectionString("CRMDatabase"))
            );
            services.AddScoped<IPisteService, PisteService>();
            return services;
        }
    }
} 
