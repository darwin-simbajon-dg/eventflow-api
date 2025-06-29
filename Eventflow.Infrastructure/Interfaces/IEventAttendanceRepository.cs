using Eventflow.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Interfaces
{
    public interface IEventAttendanceRepository
    {
        Task<bool> ConfirmAttendance(string userId, string eventId);
        Task<IEnumerable<UserEventAttendance>> GetEventAttendanceByUserId(Guid userId);
        Task<bool> RegisterForEventAsync(RegisterEvent registerEvent);
    }
}
