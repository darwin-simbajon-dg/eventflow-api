using Dapper;
using Eventflow.Domain.Aggregates.UserAggregate;
using Eventflow.Domain.Entities;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using Eventflow.Infrastructure.Data;
using Eventflow.Infrastructure.Data.Models;
using Eventflow.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventflow.Infrastructure.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly DapperDbContext _context;

        public ProfileRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProfile(Profile profile)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO Profile (UserId, StudentNumber, Firstname, Lastname, College, Email, AlternativeEmail) 
                VALUES (@UserId, @StudentNumber, @Firstname, @Lastname, @College, @Email, @AlternativeEmail)";
            
            var totalAffectedRows = await connection.ExecuteAsync(query, new 
            { 
                UserId = profile.UserId,
                StudentNumber = profile.StudentNumber,
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                College = profile.College,
                Email = profile.Email.ToString(),
                AlternativeEmail = profile.AlternativeEmail?.ToString() ?? (object)DBNull.Value
            });

            return totalAffectedRows > 0;
        }

        public async Task<ProfileModel?> GetProfileByEmail(string email)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Profile WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<ProfileModel>(query, new { Email = email });
        }

        public async Task<ProfileModel?> GetProfileByUserId(Guid userId)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Profile WHERE UserId = @UserId";
            return await connection.QueryFirstOrDefaultAsync<ProfileModel>(query, new { UserId = userId });
        }

        public Task<bool> UpdateProfile(ProfileModel existingProfile)
        {
            using var connection = _context.CreateConnection();
            var query = @"UPDATE Profile 
                          SET Firstname = @Firstname, 
                              Lastname = @Lastname, 
                              StudentNumber = @StudentNumber, 
                              College = @College, 
                              Email = @Email, 
                              AlternativeEmail = @AlternativeEmail, 
                              ImageUrl = @ImageUrl 
                          WHERE UserId = @UserId";

            var totalAffectedRows = connection.Execute(query, new
            {
                UserId = existingProfile.UserId,
                Firstname = existingProfile.Firstname,
                Lastname = existingProfile.Lastname,
                StudentNumber = existingProfile.StudentNumber,
                College = existingProfile.College,
                Email = existingProfile.Email,
                AlternativeEmail = existingProfile.AlternativeEmail,
                ImageUrl = existingProfile.ImageUrl ?? (object)DBNull.Value
            });

            return Task.FromResult(totalAffectedRows > 0);
        }
    }
}
