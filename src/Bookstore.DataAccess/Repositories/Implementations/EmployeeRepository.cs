using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using NSubstitute.Extensions;
using System.Numerics;

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
            return await _databaseContext.Employees.Where(employee => employee.Id == id).SingleOrDefaultAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var newEmployee = (await _databaseContext.Employees.AddAsync(employee)).Entity;

            await _databaseContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task<Employee> UpdateEmployeePhoneAsync(Guid id, string phone)
        {
            var updatedEmployee = await _databaseContext.Employees.Where(employee => employee.Id == id).SingleOrDefaultAsync();

            if(updatedEmployee != null)
            {
                updatedEmployee.Phone = phone;
                await _databaseContext.SaveChangesAsync();
            }
            return updatedEmployee;
        }

        public async Task<Employee> UpdateEmployeeIsActiveAsync(Guid id, bool isActive)
        {
            var updatedEmployee = await _databaseContext.Employees.Where(employee => employee.Id == id).SingleOrDefaultAsync();

            if (updatedEmployee != null)
            {
                updatedEmployee.IsActive = isActive;
                await _databaseContext.SaveChangesAsync();
            }
            return updatedEmployee;
        }
    }
}
