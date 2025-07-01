using Dapper;
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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly DapperDbContext _context;

        public UserRoleRepository(DapperDbContext dapperDbContext)
        {
            _context = dapperDbContext; 
        }

        public async Task CreateRoleforUserId(Guid userId)
        {
            using var connection = _context.CreateConnection();
            var command = "INSERT INTO [UserRole] (UserId, RoleId) VALUES (@UserId, '156FEB72-1F6E-4C29-9E21-E6D84B3C2700')";

            await connection.ExecuteAsync(command, new { UserId = userId });
        }

        public async Task<UserRole> GetUserRoleById(Guid userId)
        {
            using var connection = _context.CreateConnection();
            var query = $"SELECT u.UserId, r.RoleName FROM [User] u INNER JOIN UserRole ur " +
                $"ON u.UserId = ur.UserId " +
                $"INNER JOIN [Role] r ON ur.RoleId = r.RoleId  " +
                $"WHERE u.UserId = @UserId";
            return await connection.QueryFirstOrDefaultAsync<UserRole>(query, new { UserId = userId });
        }
    }
}
