using Dapper;
using Eventflow.Domain.Entities;
using Eventflow.Domain.Interfaces;
using Eventflow.Domain.ValueObjects;
using Eventflow.Infrastructure.Data;
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

        public async Task<User?> GetByEmailAsync(Email email)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Users WHERE Email = @Email";
            return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
        }

        public async Task AddAsync(User user)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO Users (Id, Email, PasswordHash) VALUES (@Id, @Email, @PasswordHash)";
            await connection.ExecuteAsync(query, user);
        }
    }
}
