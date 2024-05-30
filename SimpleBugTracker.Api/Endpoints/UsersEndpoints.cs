using SimpleBugTracker.Application.Users.Queries;
using MediatR;

namespace SimpleBugTracker.API.Endpoints
{
    public static class UsersEndpoints
    {
        public static void MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/users");

            group.MapGet("", GetUsers);
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

    }
}
