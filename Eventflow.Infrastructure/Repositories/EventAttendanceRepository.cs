using Dapper;
using Eventflow.Domain.Aggregates.UserAggregate;
using Eventflow.Infrastructure.Data;
using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Repositories
{
    public class EventAttendanceRepository : IEventAttendanceRepository
    {
        private readonly DapperDbContext _context;

        public EventAttendanceRepository(DapperDbContext dapperDbContext)
        {
            _context = dapperDbContext;
        }

        public async Task<bool> ConfirmAttendance(string userId, string eventId)
        {
            var userGuidId = Guid.Parse(userId);
            var eventGuidId = Guid.Parse(eventId);

            using var connection = _context.CreateConnection();
            var query = "UPDATE [EventAttendance] SET Attended = 1 WHERE UserId = @UserId AND EventId = @EventId";

            var affectedRows = await connection.ExecuteAsync(query, new { UserId = userGuidId, EventId = eventGuidId });

            return affectedRows > 0;
        }

        public async Task<IEnumerable<UserEventAttendance>> GetEventAttendanceByUserId(Guid userId)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM [EventAttendance] WHERE UserId = @UserId";

            var attendance = await connection.QueryAsync<UserEventAttendance>(query, new { UserId = userId});

            return attendance;
        }

        public async Task<bool> RegisterForEventAsync(RegisterEvent registerEvent)
        {
            using var connection = _context.CreateConnection();
            var query = "INSERT INTO EventAttendance " +
                " (AttendanceId, EventId, UserId, DateRegistered, Attended) " +
                " VALUES (NEWID(), @EventId, @UserId, GETDATE(), 0)";

            var impactedRows = await connection.ExecuteAsync(query, new { UserId = registerEvent.UserId, EventId = registerEvent.EventId });

            return impactedRows > 0;
        }
    }
}
