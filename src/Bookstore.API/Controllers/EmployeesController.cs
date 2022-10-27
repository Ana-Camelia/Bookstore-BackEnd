using Bookstore.Application.Models.Employee;
using Bookstore.Application.Services;
using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Exceptions;
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
            return Ok(ApiResponse<List<EmployeeResponseModel>>.Success(response));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeByIdAsync(Guid id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(ApiResponse<EmployeeResponseModel>.Success(response));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeAsync(EmployeeRequestModel employee)
        {
            try
            {
                var response = await _employeeService.CreateEmployeeAsync(employee);
                return Ok(ApiResponse<EmployeeResponseModel>.Success(response));
            }
            catch(EmployeeAlreadyExistsException e)
            {
                return Conflict(ApiResponse<EmployeeResponseModel>.Fail(new List<ValidationError>
                    { new("Id", e.Message) }));
            }
            catch(RoleNotFoundException e)
            {
                return Conflict(ApiResponse<EmployeeResponseModel>.Fail(new List<ValidationError>
                    { new("Role", e.Message) }));
            }
        }

        [HttpPatch("settings/{id}")]
        public async Task<IActionResult> UpdateEmployeePhoneAsync(Guid id, string phone)
        {
            try
            {
                var response = await _employeeService.UpdateEmployeePhoneAsync(id, phone);
                return Ok(ApiResponse<EmployeeResponseModel>.Success(response));
            }
            catch(EmployeeNotFoundException e)
            {
                return NotFound(ApiResponse<EmployeeResponseModel>.Fail(new List<ValidationError>
                    { new(null, e.Message) }));
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEmployeeIsActiveAsync(Guid id, bool isActive)
        {
            try
            {
                var response = await _employeeService.UpdateEmployeeIsActiveAsync(id, isActive);
                return Ok(ApiResponse<EmployeeResponseModel>.Success(response));
            }
            catch (EmployeeNotFoundException e)
            {
                return NotFound(ApiResponse<EmployeeResponseModel>.Fail(new List<ValidationError>
                    { new(null, e.Message) }));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeByIdAsync(Guid id)
        {
            try
            {
                await _employeeService.DeleteEmployeeByIdAsync(id);
                return Ok(ApiResponse<string>.Success("Action completed successfully"));
            }
            catch (EmployeeNotFoundException e)
            {
                return NotFound(ApiResponse<string>.Fail(new List<ValidationError>
                    { new(null, e.Message) }));
            }
        }
    }
}
