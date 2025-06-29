using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.UpdateEvent
{
    public class UpdateEventHandler : IRequestHandler<UpdateEventCommand, Result<bool>>
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Result<bool>> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var eventModel = new EventModel()
                {
                    EventId = Guid.Parse(command.UpdateEventRequest.EventId),
                    EventName = command.UpdateEventRequest.Title,
                    EventSchedule = GetDate(command.UpdateEventRequest.Date, command.UpdateEventRequest.Time),
                    EventHeadline = command.UpdateEventRequest.Headline,
                    Location = command.UpdateEventRequest.Venue,
                    EventDetails = command.UpdateEventRequest.Description,
                    Notes = command.UpdateEventRequest.Notes,
                    ImageUrl = command.UpdateEventRequest.ImageUrl
                };
                var isUpdated = await _eventRepository.UpdateEvent(eventModel);

                if (!isUpdated)
                {
                    return Result<bool>.Failure("Failed to update event.");
                }

                return Result<bool>.Success(true);

            }
            catch (Exception ex)
            {
                return Result<bool>.Failure("Failed to update event.");
            }

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
