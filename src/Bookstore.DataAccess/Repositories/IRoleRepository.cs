using Bookstore.DataAccess.Entities;

namespace Bookstore.DataAccess.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByNameAsync(string name);
    }
}
