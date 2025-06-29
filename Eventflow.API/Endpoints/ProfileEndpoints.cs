using EventFlow.Application.Commands.UpdateProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Eventflow.API.Endpoints
{
    public static class ProfileEndpoints
    {
        public static void MapProfileEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPut("/api/profile/{userId}", async (
                [FromRoute] Guid userId,
                [FromBody] UpdateProfileRequest request,
                ISender mediator) =>
            {
                if (!string.IsNullOrEmpty(request.ImageBase64))
                {
                    var base64Data = Regex.Match(request.ImageBase64, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    var imageBytes = Convert.FromBase64String(base64Data);

                    // Now store `imageBytes` into a `VARBINARY` or `bytea` field in your DB using Dapper/EF
                }


                var command = new UpdateProfileCommand(
                    userId,
                    request.FullName,
                    request.StudentNumber,
                    request.College,
                    request.Email,
                    request.AlternateEmail,
                    request.ImageBase64);

                var result = await mediator.Send(command);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("UpdateProfile")
            .WithTags("Profile")
            .WithOpenApi();
        }
    }
}
