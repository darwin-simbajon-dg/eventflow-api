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
