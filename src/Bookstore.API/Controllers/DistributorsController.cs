using Bookstore.Application.Services;
using Bookstore.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DistributorsController : ControllerBase
    {
        private readonly IDistributorService _distributorService;

        public DistributorsController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDistributorsAsync()
        {
            var response = await _distributorService.GetDistributorsAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistributorByIdAsync(Guid id)
        {
            var response = await _distributorService.GetDistributorByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistributorAsync(Distributor distributor)
        {
            var response = await _distributorService.CreateDistributorAsync(distributor);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistributorAsync(Distributor distributor)
        {
            var response = await _distributorService.UpdateDistributorAsync(distributor);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistributorByIdAsync(Guid id)
        {
            await _distributorService.DeleteDistributorByIdAsync(id);
            return Ok("Action completed successfully");
        }
    }
}
