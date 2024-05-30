using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBugTracker.Infrastructure.Data;
using SimpleBugTracker.Application.Data;
using Ardalis.GuardClauses;

namespace SimpleBugTracker.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("applicationConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'applicationConnection' not found.");


            services.AddDbContext<ApplicationDbContext>((options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            return services;
        }
    }
}
