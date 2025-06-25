using EventFlow.Application.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace Eventflow.API.Endpoints
{
    public static class UserEndpoints
    {
        public async static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/register", async (RegisterRequest request, ISender mediator) =>
            {
                var command = new RegisterUserCommand(request.Firstname, request.Lastname, request.Email, request.Password);
                var result = await mediator.Send(command);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
           .WithName("RegisterUser")
           .WithOpenApi();

            app.MapPost("/api/auth/login", async (LoginRequest request) =>
            {
                // TODO: Validate login and return token
                if (request.Email == "test@example.com" && request.Password == "password")
                {
                    return Results.Ok(new { token = "mock-jwt-token" });
                }

                return Results.Unauthorized();
            })
            .WithName("LoginUser")
            .WithOpenApi();
        }

        public record RegisterRequest(string Email, string Password, string Firstname, string Lastname);
        public record LoginRequest(string Email, string Password);
    }
}
