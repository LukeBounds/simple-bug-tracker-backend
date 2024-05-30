using SimpleBugTracker.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleBugTracker.Application.Users.Commands;

namespace SimpleBugTracker.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static void MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/users");

            group.MapGet("", GetUsers);
            group.MapPost("", CreateUser);
            group.MapPut("", UpdateUser);
        }

        public static async Task<IResult> GetUsers(ISender sender, [AsParameters] GetUsersQuery query)
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

        public static async Task<IResult> CreateUser(ISender sender, [FromBody] CreateUserCommand command)
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

        public static async Task<IResult> UpdateUser(ISender sender, [FromBody] UpdateUserCommand command)
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
