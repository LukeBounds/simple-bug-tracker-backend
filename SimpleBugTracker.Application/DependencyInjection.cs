using Microsoft.Extensions.DependencyInjection;
using SimpleBugTracker.Application.Users.Services;
using System.Reflection;

namespace SimpleBugTracker.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddSingleton<IUserService, UserService>();

        return services;
    }
}
