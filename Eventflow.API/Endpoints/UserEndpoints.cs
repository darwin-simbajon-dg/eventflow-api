﻿using EventFlow.Application.Commands.RegisterUser;
using EventFlow.Application.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eventflow.API.Endpoints
{
    public static class UserEndpoints
    {
        public async static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/register", async ([FromBody] RegisterRequest request, 
                ISender mediator,
                ILoggerFactory loggerFactory) =>
            {
                try
                {
                    var logger = loggerFactory.CreateLogger("UserEndpoints");
                    logger.LogInformation("Register event with {data}", JsonConvert.SerializeObject(request));

                    var command = new RegisterUserCommand(
                                       request.StudentNumber,
                                       request.Firstname,
                                       request.Lastname,
                                       request.College,
                                       request.Email,
                                       request.AlternativeEmail,
                                       request.Password
                                     );

                    var result = await mediator.Send(command);

                    if (result.IsSuccess)
                    {
                        logger.LogInformation("Successful registration with response {data}", JsonConvert.SerializeObject(result.Value));
                    }
                    else
                    {
                        logger.LogInformation("Failed registration with response {data}", JsonConvert.SerializeObject(result.Errors));
                    }
                    

                    return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }

               
            })
           .WithName("RegisterUser")
           .WithOpenApi();

            app.MapPost("/api/auth/login", async ([FromBody] LoginRequest request, ISender mediator) =>
            {
                var query = new LoginUserQuery(request.Username, request.Password);
                var result = await mediator.Send(query);

                return result != null ? Results.Ok(result.Value) : Results.Unauthorized();
            })
            .WithName("LoginUser")
            .WithOpenApi();
        }

        public record RegisterRequest(string Email, string Password, int StudentNumber, string Firstname, string Lastname, string College, string AlternativeEmail);
        public record LoginRequest(string Username, string Password);
    }
}
