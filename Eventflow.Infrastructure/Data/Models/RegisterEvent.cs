using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class RegisterEvent
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
    }
}
