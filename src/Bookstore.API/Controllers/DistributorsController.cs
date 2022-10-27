using Bookstore.Application.Exceptions;
using Bookstore.Application.Models.Employee;
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
            return Ok(ApiResponse<List<Distributor>>.Success(response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistributorByIdAsync(Guid id)
        {
            var response = await _distributorService.GetDistributorByIdAsync(id);
            return Ok(ApiResponse<Distributor>.Success(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDistributorAsync(Distributor distributor)
        {
            try
            {
                var response = await _distributorService.CreateDistributorAsync(distributor);
                return Ok(ApiResponse<Distributor>.Success(response));
            }
            catch(DistributorAlreadyExistsException e)
            {
                return Conflict(ApiResponse<Distributor>.Fail(new List<ValidationError>
                    { new("Id", e.Message) }));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDistributorAsync(Distributor distributor)
        {
            try
            {
                var response = await _distributorService.UpdateDistributorAsync(distributor);
                return Ok(ApiResponse<Distributor>.Success(response));
            }
            catch (DistributorNotFoundException e)
            {
                return NotFound(ApiResponse<Distributor>.Fail(new List<ValidationError>
                    { new(null, e.Message) }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistributorByIdAsync(Guid id)
        {
            try
            {
                await _distributorService.DeleteDistributorByIdAsync(id);
                return Ok(ApiResponse<string>.Success("Action completed successfully"));
            }
            catch (DistributorNotFoundException e)
            {
                return NotFound(ApiResponse<string>.Fail(new List<ValidationError>
                    { new(null, e.Message) }));
            }
        }
    }
}
