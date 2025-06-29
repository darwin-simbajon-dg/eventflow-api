using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Entities
{
    public class Role
    {
        public Guid RoleId { get; set; }

        public string RoleName { get; set; }
    }
}
