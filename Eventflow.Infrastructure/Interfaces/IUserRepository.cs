using Eventflow.Domain.Aggregates.UserAggregate;
using Eventflow.Domain.Entities;
using Eventflow.Domain.ValueObjects;
using Eventflow.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel?> GetByEmailAsync(Email email);
        Task<bool> AddAsync(User user);
        Task<ProfileModel> VerifyUser(Email email, string password);
    }
}
