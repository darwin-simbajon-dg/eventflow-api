using Azure.Core;
using Eventflow.Domain.Aggregates.UserAggregate;
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
            app.MapGet("/api/events/{userId}", async ([FromRoute]Guid userId, ISender mediator, 
                IHubContext<NotificationHub> hubContext,
                ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("EventEndpoints");
                logger.LogInformation("Fetching events for user {UserId}", userId);

                var query = new GetEventsQuery(userId);
                var result = await mediator.Send(query);

                var jsonData = JsonConvert.SerializeObject(result.Value);
                logger.LogInformation("Fetched events for user {UserId}", userId);
                logger.LogInformation("Data: {data}", jsonData);

                return result != null ? Results.Ok(result.Value) : Results.NotFound();
            })
            .WithName("GetAllEvents")
            .WithTags("Events")
            .WithOpenApi();


            app.MapPost("api/event/register", async ([FromBody] RegisterEventRequest request, 
                ISender mediator, 
                IHubContext<NotificationHub> hubContext,
                ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("EventEndpoints");
                var jsonData = JsonConvert.SerializeObject(request);
                logger.LogInformation("Register event with data {UserId}", jsonData);

                var command = new RegisterEventCommand(request.EventId, request.UserId);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    logger.LogInformation("Send EventListUpdated Event from SignalR");
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                jsonData = JsonConvert.SerializeObject(result.Value);
                logger.LogInformation("Register event response {data}", jsonData);
                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("RegisterEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapPost("api/event", async ([FromBody] CreateEventRequest request, 
                ISender mediator,
                IHubContext<NotificationHub> hubContext,
                ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("EventEndpoints");
                var jsonData = JsonConvert.SerializeObject(request);
                logger.LogInformation("Create event with data {request}", jsonData);

                var command = new CreateEventCommand(request);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                jsonData = JsonConvert.SerializeObject(result.Value);
                logger.LogInformation("Create event response {data}", jsonData);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("CreateEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapDelete("api/event/{eventId}", async ([FromRoute] Guid eventId, 
                ISender mediator, 
                IHubContext<NotificationHub> hubContext,
                ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("EventEndpoints");
                logger.LogInformation("Delete event with data {eventId}", eventId);

                var command = new DeleteEventCommand(eventId);
                var result = await mediator.Send(command);
                if (result.IsSuccess)
                {
                    logger.LogInformation("Send EventListUpdated Event from SignalR");
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                var jsonData = JsonConvert.SerializeObject(result.Value);
                logger.LogInformation("Delete event response {data}", jsonData);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("DeleteEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapPut("api/event", async ([FromBody] UpdateEventRequest request, 
                ISender mediator, 
                IHubContext<NotificationHub> hubContext,
                ILoggerFactory loggerFactory) =>
            {
                var logger = loggerFactory.CreateLogger("EventEndpoints");
                var jsonData = JsonConvert.SerializeObject(request);
                logger.LogInformation("Update event with data {request}", jsonData);

                var command = new UpdateEventCommand(request);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    logger.LogInformation("Send EventListUpdated Event from SignalR");
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                jsonData = JsonConvert.SerializeObject(result.Value);
                logger.LogInformation("Delete event response {data}", jsonData);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
            })
            .WithName("UpdateEvent")
            .WithTags("Events")
            .WithOpenApi();

            app.MapPost("api/event/confirm-attendance", async ([FromBody] ConfirmAttendanceRequest request, 
                ISender mediator, 
                IHubContext<NotificationHub> hubContext,
                ILoggerFactory loggerFactory) =>
            {

                var logger = loggerFactory.CreateLogger("EventEndpoints");
                var jsonData = JsonConvert.SerializeObject(request);
                logger.LogInformation("Confirm attendance with data {request}", jsonData);

                var command = new ConfirmAttendanceCommand(request);
                var result = await mediator.Send(command);

                if (result.IsSuccess)
                {
                    logger.LogInformation("Send EventListUpdated Event from SignalR");
                    await hubContext.Clients.All.SendAsync("ReceiveMessage", "EventListUpdated");
                }

                jsonData = JsonConvert.SerializeObject(result.Value);
                logger.LogInformation("Confirm attendance response {data}", jsonData);

                return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);

            })
            .WithName("ConfirmAttendance")
            .WithTags("Events")
            .WithOpenApi();
        }
    }
}
