using Eventflow.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Interfaces
{
    public interface IUserRoleRepository
    {
        Task CreateRoleforUserId(Guid userId);
        Task<UserRole> GetUserRoleById(Guid userId);
    }
}
