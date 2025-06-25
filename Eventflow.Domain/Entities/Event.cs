using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime ScheduledAt { get; private set; }

        public Event(string title, DateTime scheduledAt)
        {
            Id = Guid.NewGuid();
            Title = title;
            ScheduledAt = scheduledAt;
        }

        public void Reschedule(DateTime newDate) => ScheduledAt = newDate;
    }

}
