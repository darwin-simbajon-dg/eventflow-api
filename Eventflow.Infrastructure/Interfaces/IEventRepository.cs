using Eventflow.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Interfaces
{
    public interface IEventRepository
    {
        Task<bool> AddEvent(EventModel createdEvent);
        Task<EventModel?> GetEventByIdAsync(Guid eventId);
        Task<IEnumerable<EventRegistration>> GetAllEventsAsync(Guid userId);
        Task<IEnumerable<EventModel>> GetAllEventsAsync();
        Task<bool> CreateEvent(EventModel eventModel);
        Task<bool> DeleteEvent(Guid eventId);
        Task<bool> UpdateEvent(EventModel eventModel);
    }
}
