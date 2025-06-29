using EventFlow.Application.Commands.ConfirmAttendance;
using EventFlow.Application.Commands.CreateEvent;
using EventFlow.Application.Commands.DeleteEvent;
using EventFlow.Application.Commands.RegisterEvent;
using EventFlow.Application.Commands.UpdateEvent;
using EventFlow.Application.DTOs;
using EventFlow.Application.Queries.GetEvents;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Eventflow.API.Endpoints
{
    public static class EventEndpoints
    {
        public static void MapEventEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/events/{userId}", async ([FromRoute]Guid userId, ISender mediator, IHubContext<NotificationHub> hubContext) =>
            {
                var query = new GetEventsQuery(userId);
                var result = await mediator.Send(query);         

                return result != null ? Results.Ok(result.Value) : Results.NotFound();
            })
            .WithName("GetAllEvents")
            .WithTags("Events")
            .WithOpenApi();


            app.MapPost("api/event/register", async ([FromBody] RegisterEventRequest request, ISender mediator, IHubContext<NotificationHub> hubContext) =>
            {
                var command = new RegisterEventCommand(request.EventId, request.UserId);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("RegisterEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapPost("api/event", async ([FromBody] CreateEventRequest request, ISender mediator,
                IHubContext<NotificationHub> hubContext) =>
            {
                var command = new CreateEventCommand(request);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("CreateEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapDelete("api/event/{eventId}", async ([FromRoute] Guid eventId, ISender mediator, IHubContext<NotificationHub> hubContext) =>
            {
                var command = new DeleteEventCommand(eventId);
                var result = await mediator.Send(command);
                if (result.IsSuccess)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("DeleteEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapPut("api/event", async ([FromBody] UpdateEventRequest request, ISender mediator, IHubContext<NotificationHub> hubContext) =>
            {
                var command = new UpdateEventCommand(request);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("UpdateEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapPost("api/event/confirm-attendance", async ([FromBody] ConfirmAttendanceRequest request, ISender mediator, 
                IHubContext<NotificationHub> hubContext) =>
            {
                var command = new ConfirmAttendanceCommand(request);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);

            })
            .WithName("ConfirmAttendance")
            .WithTags("Events")
            .WithOpenApi();
        }
    }
}
