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
    public class UserRepository : IUserRepository
    {
        private readonly DapperDbContext _context;

        public UserRepository(DapperDbContext context)
        {
            _context = context;
        }

        public async Task<UserModel?> GetByEmailAsync(Email email)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var query = $"SELECT * FROM [User] WHERE Email = '@Email'";
                return await connection.QueryFirstOrDefaultAsync<UserModel>(query, new { Email = email.Value });
            }
            catch (Exception ex)
            {

                throw;
            }
         
        }

        public async Task<Profile> VerifyUser(Email email, string password)
        {
            var user = await GetByEmailAsync(email);
            var profile = await GetProfileByUser(user);
            if (user == null) return null;
            
            if(user.Password.Equals(password)) return profile;

            return null;
        }

        private async Task<Profile> GetProfileByUser(UserModel? user)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Profile WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<Profile>(query, new { Email = user.Email });
        }

        public async Task<bool> AddAsync(User user)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO [User] (UserId, Email, Password) VALUES (@UserId, @Email, @Password)";
            var totalAffectedRows = await connection.ExecuteAsync(query, 
                new {
                    UserId = user.UserId, 
                    Email = user.Email.Value, 
                    Password = user.Password}
                );

            return totalAffectedRows > 0;
        }
    }
}
