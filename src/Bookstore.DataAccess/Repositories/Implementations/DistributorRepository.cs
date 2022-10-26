using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Bookstore.DataAccess.Repositories.Implementations
{
    public class DistributorRepository : IDistributorRepository
    {
        private readonly DatabaseContext _databaseContext;

        public DistributorRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Distributor>> GetDistributorsAsync()
        {
            return await _databaseContext.Distributors.ToListAsync();
        }

        public async Task<Distributor> GetDistributorByIdAsync(Guid id)
        {

            return await _databaseContext.Distributors.Where(distributor => distributor.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Distributor> CreateDistributorAsync(Distributor distributor)
        {
            var newDistributor = (await _databaseContext.Distributors.AddAsync(distributor)).Entity;

            await _databaseContext.SaveChangesAsync();

            return newDistributor;
        }

        public async Task<Distributor> UpdateDistributorAsync(Distributor distributor)
        {
            var updatedDistributor = await _databaseContext.Distributors.Where(dis => dis.Id == distributor.Id).SingleOrDefaultAsync();

            if (updatedDistributor != null)
            {
                updatedDistributor.Name = distributor.Name;
                updatedDistributor.Address = distributor.Address;
                updatedDistributor.Phone = distributor.Phone;
                updatedDistributor.IsActive = distributor.IsActive;
                await _databaseContext.SaveChangesAsync();
            }
            return updatedDistributor;
        }

        public async Task DeleteDistributorByIdAsync(Guid id)
        {
            var deletedDistributor = await _databaseContext.Distributors.Where(distributor => distributor.Id == id).SingleOrDefaultAsync();
            if (deletedDistributor != null)
            {
                _databaseContext.Distributors.Remove(deletedDistributor);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
