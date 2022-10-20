using Bookstore.DataAccess.Entities;

namespace Bookstore.Application.Services
{
    public interface IDistributorService
    {
        Task<List<Distributor>> GetDistributorsAsync();
        Task<Distributor> GetDistributorByIdAsync(Guid id);
        Task<Distributor> CreateDistributorAsync(Distributor distributor);
        Task<Distributor> UpdateDistributorAsync(Distributor distributor);
        Task DeleteDistributorByIdAsync(Guid id);
    }
}
