using Eventflow.Domain.Entities;
using Eventflow.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Interfaces
{
    public interface IProfileRepository
    {
        Task<bool> AddProfile(Profile profile);
        Task<ProfileModel?> GetProfileByUserId(Guid UserId);
        Task<ProfileModel?> GetProfileByEmail(string email);
        Task<bool> UpdateProfile(ProfileModel existingProfile);
    }
}
