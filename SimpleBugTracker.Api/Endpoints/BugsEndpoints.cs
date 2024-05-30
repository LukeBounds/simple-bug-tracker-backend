using SimpleBugTracker.Application.Bugs.Queries;
using MediatR;

namespace SimpleBugTracker.API.Endpoints
{
    public static class BugsEndpoints
    {
        public static void MapBugsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/bugs");

            group.MapGet("", GetBugs);
            group.MapGet("/bug", GetBug);
        }

        public static async Task<IResult> GetBugs(ISender sender, [AsParameters] GetBugsQuery query)
        {
            try
            {
                var ret = await sender.Send(query);

                return Results.Ok(ret);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> GetBug(ISender sender, [AsParameters] GetBugQuery query)
        {
            try
            {
                var ret = await sender.Send(query);

                return Results.Ok(ret);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
