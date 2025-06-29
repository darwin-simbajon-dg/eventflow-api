using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.CreateEvent
{
   public class CreateEventHandler : IRequestHandler<CreateEventCommand, Result<bool>>
    {
        private readonly IEventRepository _eventRepository;
        public CreateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }
        public async Task<Result<bool>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventModel = new EventModel()
            {
                EventId =  string.IsNullOrEmpty(request.CreateEventRequest.EventId) 
                ? Guid.NewGuid() 
                : Guid.Parse(request.CreateEventRequest.EventId),
                EventName = request.CreateEventRequest.Title,
                EventSchedule = GetDate(request.CreateEventRequest.Date, request.CreateEventRequest.Time),
                EventHeadline = request.CreateEventRequest.Headline,
                Location = request.CreateEventRequest.Venue,
                EventDetails = request.CreateEventRequest.Description,
                Notes = request.CreateEventRequest.Notes,
                ImageUrl = request.CreateEventRequest.ImageUrl
            };
            var isCreated = await _eventRepository.CreateEvent(eventModel);
            if (!isCreated)
            {
                return Result<bool>.Failure("Failed to create event.");
            }
            return Result<bool>.Success(true);
        }

        private DateTime GetDate(string date, string time)
        {
            if (!DateTime.TryParse(date, out var parsedDate))
                throw new FormatException($"Invalid date format: {date}");

            if (!TimeSpan.TryParse(time, out var parsedTime))
                throw new FormatException($"Invalid time format: {time}");

            return parsedDate.Date + parsedTime;
        }

    }
}
