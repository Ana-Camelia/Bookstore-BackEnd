using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Exceptions;
using Bookstore.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DataAccess.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DatabaseContext _databaseContext;

        public EmployeeRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _databaseContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id)
        {
            return await _databaseContext.Employees.Where(emp => emp.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var employeeToAdd = (await _databaseContext.Employees.AddAsync(employee)).Entity;

            await _databaseContext.SaveChangesAsync();

            return employeeToAdd;
        }

        public async Task<Employee> UpdateEmployeePhoneAsync(Employee employeeToUpdate, string phone)
        {
            if (employeeToUpdate != null)
            {
                employeeToUpdate.Phone = phone;
                await _databaseContext.SaveChangesAsync();
            }

            return employeeToUpdate;
        }

        public async Task<Employee> UpdateEmployeeIsActiveAsync(Employee employeeToUpdate, bool isActive)
        {
            if (employeeToUpdate != null)
            {
                employeeToUpdate.IsActive = isActive;
                await _databaseContext.SaveChangesAsync();
            }

            return employeeToUpdate;
        }

        public async Task DeleteEmployeeByIdAsync(Employee employeeToDelete)
        {
            if (employeeToDelete != null)
            {
                _databaseContext.Employees.Remove(employeeToDelete);
                await _databaseContext.SaveChangesAsync();
            }
        }
    }
}
