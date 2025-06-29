using Eventflow.Infrastructure.Interfaces;
using Eventflow.Shared;
using EventFlow.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.Application.Queries.GetEvents
{
    public class GetEventsHandler : IRequestHandler<GetEventsQuery, Result<IEnumerable<EventDTO>>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IEventAttendanceRepository _eventAttendanceRepository;

        public GetEventsHandler(IEventRepository eventRepository, IUserRoleRepository userRoleRepository,
            IEventAttendanceRepository eventAttendanceRepository)
        {
            _eventRepository = eventRepository;
            _userRoleRepository = userRoleRepository;
            _eventAttendanceRepository = eventAttendanceRepository;
        }

        public async Task<Result<IEnumerable<EventDTO>>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var events = new List<EventDTO>();

            try
            {
                var userRole = await _userRoleRepository.GetUserRoleById(request.UserId);
                var attendance = await _eventAttendanceRepository.GetEventAttendanceByUserId(request.UserId);
                var allEvents = await _eventRepository.GetAllEventsAsync();

                if (userRole == null || allEvents == null)
                {
                    return Result<IEnumerable<EventDTO>>.Failure("No events found or user role not found.");
                }

                if(userRole.RoleName == "Admin")
                {
                    // If the user is an admin, return all events
                    // filter depending on the configuration
                    events = allEvents.Select(e => (EventDTO)e).ToList();
                    return Result<IEnumerable<EventDTO>>.Success(events);
                }
                else
                {
                    foreach (var eventItem in allEvents)
                    {
                        EventDTO eventDTO = eventItem;

                        var foundAttendance = attendance.FirstOrDefault(a => a.EventId == eventItem.EventId);

                        if(foundAttendance != null)
                        {
                            eventDTO.IsRegistered = true;
                            eventDTO.Attended = foundAttendance.Attended;
                        }
 
                        events.Add(eventDTO);
                    }

                    return Result<IEnumerable<EventDTO>>.Success(events);                 
                }

                

            }
            catch (Exception)
            {

                return Result<IEnumerable<EventDTO>>.Failure("Failed to retrieve events.");
            }

            //Get all events
            //Get User role
            //If Admin display only based on the configuration
            //If User get the attendance record
            //Construct EventDTO from EventRegistration or EventModel



        }
    }
}
