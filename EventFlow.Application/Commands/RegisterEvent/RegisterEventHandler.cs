using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Commands.RegisterEvent
{
    public class RegisterEventHandler : IRequestHandler<RegisterEventCommand, Result<bool>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventAttendanceRepository _eventAttendanceRepository;
        public RegisterEventHandler(IEventRepository eventRepository, IEventAttendanceRepository eventAttendanceRepository)
        {
            _eventRepository = eventRepository;
            _eventAttendanceRepository = eventAttendanceRepository;
        }
        public async Task<Result<bool>> Handle(RegisterEventCommand request, CancellationToken cancellationToken)
        {
            var eventItem = await _eventRepository.GetEventByIdAsync(request.EventId);
            if (eventItem == null)
            {
                return Result<bool>.Failure("Event not found.");
            }
            var registerEvent = new Eventflow.Infrastructure.Data.Models.RegisterEvent
            {
                EventId = request.EventId,
                UserId = request.UserId,
            };

            var isSuccessful = await _eventAttendanceRepository.RegisterForEventAsync(registerEvent);
            return isSuccessful
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Failed to register for the event.");
        }
    }
 
}
