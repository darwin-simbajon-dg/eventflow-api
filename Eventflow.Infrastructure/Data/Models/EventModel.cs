using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class EventModel
    {
        public Guid EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventSchedule { get; set; }
        public string EventHeadline { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public string EventDetails { get; set; }
        public string ImageUrl { get; set; }      
        public DateTime DateCreated { get; set; }

    }
}
