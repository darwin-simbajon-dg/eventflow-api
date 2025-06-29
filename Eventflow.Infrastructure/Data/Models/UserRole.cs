using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Data.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
    }
}
