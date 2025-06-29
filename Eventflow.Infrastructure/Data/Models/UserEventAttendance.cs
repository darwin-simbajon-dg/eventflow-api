using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class UserEventAttendance
    {
        public Guid UserId { get; set; }

        public Guid EventId { get; set; }

        public bool IsRegistered { get; set; }

        public bool Attended { get; set; }  
    }
}
