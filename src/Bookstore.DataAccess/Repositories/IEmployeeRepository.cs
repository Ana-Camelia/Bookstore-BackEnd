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
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<Employee> UpdateEmployeePhoneAsync(Guid id, string phone);
        Task<Employee> UpdateEmployeeIsActiveAsync(Guid id, bool isActive);
    }
}
