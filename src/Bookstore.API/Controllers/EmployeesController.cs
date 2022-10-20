using Bookstore.Application.Models.Employee;
using Bookstore.Application.Services;
using Bookstore.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Core;

namespace Bookstore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesAsync()
        {
            var response = await _employeeService.GetEmployeesAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeRequestModel employee)
        {
            var response = await _employeeService.CreateEmployeeAsync(employee);
            return Ok(response);
        }

        [HttpPatch("settings/{id}")]
        public async Task<IActionResult> UpdateEmployeePhoneAsync(Guid id, string phone)
        {
            var response = await _employeeService.UpdateEmployeePhoneAsync(id, phone);
            return Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployeeIsActiveAsync(Guid id, bool isActive)
        {
            var response = await _employeeService.UpdateEmployeeIsActiveAsync(id, isActive);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(Guid id)
        {
            await _employeeService.DeleteEmployeeByIdAsync(id);
            return Ok("Action completed successfully");
        }
    }
}
