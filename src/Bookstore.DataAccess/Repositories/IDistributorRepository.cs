using Bookstore.DataAccess.Entities;

namespace Bookstore.DataAccess.Repositories
{
    public interface IDistributorRepository
    {
        Task<List<Distributor>> GetDistributorsAsync();
        Task<Distributor> GetDistributorByIdAsync(Guid id);
        Task<Distributor> CreateDistributorAsync(Distributor distributor);
        Task<Distributor> UpdateDistributorAsync(Distributor distributor);
        Task DeleteDistributorByIdAsync(Guid id);
    }
}
