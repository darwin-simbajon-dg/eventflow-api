using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class EventRegistration
    {
        public Guid EventId { get; set; }

        public string EventName { get; set; }

        public DateTime DateCreated { get; set; }

        public string EventDetails { get; set; }

        public bool IsRegistered { get; set; }
    }
}
