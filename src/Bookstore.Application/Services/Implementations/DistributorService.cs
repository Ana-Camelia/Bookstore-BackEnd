using Bookstore.Application.Exceptions;
using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Exceptions;
using Bookstore.DataAccess.Repositories;
using Bookstore.DataAccess.Repositories.Implementations;

namespace Bookstore.Application.Services.Implementations
{
    public class DistributorService : IDistributorService
    {
        private readonly IDistributorRepository _distributorRepository;

        public DistributorService(IDistributorRepository distributorRepository)
        {
            _distributorRepository = distributorRepository;
        }

        public async Task<List<Distributor>> GetDistributorsAsync()
        {
            return await _distributorRepository.GetDistributorsAsync();
        }

        public async Task<Distributor> GetDistributorByIdAsync(Guid id)
        {
            return await _distributorRepository.GetDistributorByIdAsync(id);
        }

        public async Task<Distributor> CreateDistributorAsync(Distributor distributor)
        {
            var searchResult = await _distributorRepository.GetDistributorByIdAsync(distributor.Id);
            if (searchResult != null)
                throw new DistributorAlreadyExistsException("A distributor with this ID already exists.");

            return await _distributorRepository.CreateDistributorAsync(distributor);
        }

        public async Task<Distributor> UpdateDistributorAsync(Distributor distributor)
        {
            var searchResult = await _distributorRepository.GetDistributorByIdAsync(distributor.Id);
            if (searchResult == null)
                throw new DistributorAlreadyExistsException("This distributor does not exist.");

            return await _distributorRepository.UpdateDistributorAsync(distributor);
        }

        public async Task DeleteDistributorByIdAsync(Guid id)
        {
            var searchResult = await _distributorRepository.GetDistributorByIdAsync(id);
            if (searchResult == null)
                throw new DistributorAlreadyExistsException("This distributor does not exist.");

            await _distributorRepository.DeleteDistributorByIdAsync(id);
        }
    }
}
