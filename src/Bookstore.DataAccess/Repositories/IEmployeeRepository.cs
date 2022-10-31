using Bookstore.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DataAccess.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<Employee> GetEmployeeByCnpAsync(string cnp);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeePhoneAsync(Employee employeeToUpdate, string phone);
        Task<Employee> UpdateEmployeeIsActiveAsync(Employee employeeToUpdate, bool isActive);
        Task DeleteEmployeeByIdAsync(Employee employeeToDelete);
    }
}
