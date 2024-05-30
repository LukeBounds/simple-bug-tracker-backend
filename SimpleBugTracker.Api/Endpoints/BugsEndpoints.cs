using SimpleBugTracker.Application.Bugs.Commands;
using SimpleBugTracker.Application.Bugs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimpleBugTracker.API.Endpoints
{
    public static class BugsEndpoints
    {
        public static void MapBugsEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/bugs");

            group.MapGet("", GetBugs);
            group.MapGet("/bug", GetBug);
            group.MapPost("", CreateBug);
            group.MapPut("", UpdateBug);
            group.MapPut("/close", CloseBug);
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

        public static async Task<IResult> CreateBug(ISender sender, [FromBody] CreateBugCommand command)
        {
            try
            {
                var ret = await sender.Send(command);

                return Results.Ok(ret);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> UpdateBug(ISender sender, [FromBody] UpdateBugCommand command)
        {
            try
            {
                var ret = await sender.Send(command);

                return Results.Ok(ret);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }

        public static async Task<IResult> CloseBug(ISender sender, [FromBody] CloseBugCommand command)
        {
            try
            {
                var ret = await sender.Send(command);

                return Results.Ok(ret);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
