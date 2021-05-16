using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();

            //Create connection string for database
            services.AddDbContext<DataContext>(options => //lamda expression
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection")); //pass a connection string, target default

            });

            return services;
        }
    }
}