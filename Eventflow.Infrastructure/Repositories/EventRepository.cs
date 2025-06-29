using Dapper;
using Eventflow.Domain.Aggregates.UserAggregate;
using Eventflow.Domain.Entities;
using Eventflow.Infrastructure.Data;
using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DapperDbContext _context;

        public EventRepository(DapperDbContext context)
        {
            _context = context;
        }

        public Task<bool> AddEvent(EventModel createdEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateEvent(EventModel eventModel)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var query = @"INSERT INTO [Event] (EventId, EventName, EventSchedule, EventHeadline, Location, EventDetails, Notes, ImageUrl, DateCreated) 
                          VALUES (@EventId, @EventName, @Schedule, @Headline, @Location, @EventDetails, @Notes, @ImageUrl, GETDATE())";

                var affectedRows = await connection.ExecuteAsync(query, new
                {
                    EventId = eventModel.EventId,
                    EventName = eventModel.EventName,
                    Schedule = eventModel.EventSchedule,
                    Headline = eventModel.EventHeadline,
                    Location = eventModel.Location,
                    EventDetails = eventModel.EventDetails,
                    Notes = eventModel.Notes,
                    ImageUrl = eventModel.ImageUrl
                });

                return affectedRows > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }

           
        }

        public async Task<bool> DeleteEvent(Guid eventId)
        {
            using var connection = _context.CreateConnection();
            var query = "DELETE FROM [Event] WHERE EventId = @EventId";
            var affectedRows = await connection.ExecuteAsync(query, new { EventId = eventId });

            return affectedRows > 0;
        }

        public async Task<IEnumerable<EventRegistration>> GetAllEventsAsync(Guid userId)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT e.EventId EventId, e.EventName EventName, e.DateCreated DateCreated, e.EventDetails EventDetails,  " +
                "CASE WHEN u.UserId IS NOT NULL THEN 1 ELSE 0 END AS IsRegistered " +
                "FROM [Event] e LEFT JOIN EventAttendance ea ON e.EventId = ea.EventId " +
                "AND ea.UserId = @UserId LEFT JOIN[User] u ON u.UserId = ea.UserId";

            var events = await connection.QueryAsync<EventRegistration>(query, new { UserId = userId });

            return events ?? new List<EventRegistration>();
        }

        public async Task<IEnumerable<EventModel>> GetAllEventsAsync()
        {
           using var connection = _context.CreateConnection();
           var query = "SELECT * FROM [Event]";

           var events = await connection.QueryAsync<EventModel>(query);

           return events;
        }

        public async Task<EventModel?> GetEventByIdAsync(Guid eventId)
        {

            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM [Event] WHERE EventId = @EventId";

            var events = await connection.QueryFirstOrDefaultAsync<EventModel>(query, new { EventId = eventId});

            return events;
        }

        public async Task<bool> UpdateEvent(EventModel eventModel)
        {
            using var connection = _context.CreateConnection();
            var query = @"UPDATE [Event] 
                          SET EventName = @EventName, 
                              EventSchedule = @Schedule, 
                              EventHeadline = @Headline, 
                              Location = @Location, 
                              EventDetails = @EventDetails, 
                              Notes = @Notes, 
                              ImageUrl = @ImageUrl 
                          WHERE EventId = @EventId";

            var affectedRows = await connection.ExecuteAsync(query, new
            {
                EventId = eventModel.EventId,
                EventName = eventModel.EventName,
                Schedule = eventModel.EventSchedule,
                Headline = eventModel.EventHeadline,
                Location = eventModel.Location,
                EventDetails = eventModel.EventDetails,
                Notes = eventModel.Notes,
                ImageUrl = eventModel.ImageUrl
            });

            return affectedRows > 0;
        }
    }
}
