using Bookstore.Application.Models.Employee;
using Bookstore.DataAccess.Entities;

namespace Bookstore.Application.Services
{
    public interface IEmployeeService
    {
        Task<List<EmployeeResponseModel>> GetEmployeesAsync();
        Task<EmployeeResponseModel> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeResponseModel> CreateEmployeeAsync(EmployeeRequestModel employee);
        Task<EmployeeResponseModel> UpdateEmployeePhoneAsync(Guid id, string phone);
        Task<EmployeeResponseModel> UpdateEmployeeIsActiveAsync(Guid id, bool isActive);
        Task DeleteEmployeeByIdAsync(Guid id);
    }
}
