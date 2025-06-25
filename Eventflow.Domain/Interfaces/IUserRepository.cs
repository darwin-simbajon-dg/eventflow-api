using Eventflow.Domain.Entities;
using Eventflow.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(Email email);
        Task AddAsync(User user);
    }
}
