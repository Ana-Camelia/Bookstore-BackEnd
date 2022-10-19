using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DataAccess.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DatabaseContext _databaseContext;

        public RoleRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<Role> GetRoleByNameAsync(string name)
        {
            return await _databaseContext.Roles.Where(role => role.Name == name).SingleOrDefaultAsync();
        }
    }
}
