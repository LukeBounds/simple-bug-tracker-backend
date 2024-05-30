
namespace SimpleBugTracker.API.Endpoints;
public static class DependencyInjection
{
    public static IEndpointRouteBuilder AddApiEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUsersEndpoints();
        app.MapBugsEndpoints();

        return app;
    }
}
