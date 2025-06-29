using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class EventAttendanceModel
    {
        public Guid AttendanceId { get; set; }

        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public DateTime DateRegistered { get; set; }

        public bool Attended { get; set; }
    }
}
