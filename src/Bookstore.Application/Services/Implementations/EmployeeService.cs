using AutoMapper;
using Bookstore.Application.Models.Employee;
using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Repositories;
using System.Numerics;

namespace Bookstore.Application.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponseModel>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetEmployeesAsync();
            return _mapper.Map<List<EmployeeResponseModel>>(employees);
        }

        public async Task<EmployeeResponseModel> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }

        public async Task<EmployeeResponseModel> CreateEmployeeAsync(EmployeeRequestModel employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);
            newEmployee.Id = new Guid();
            newEmployee.Role = await _roleRepository.GetRoleByNameAsync(employee.Role);

            var employeeResponse = await _employeeRepository.CreateEmployeeAsync(newEmployee);

            return _mapper.Map<EmployeeResponseModel>(employeeResponse);
        }

        public async Task<EmployeeResponseModel> UpdateEmployeePhoneAsync(Guid id, string phone)
        {
            var employee = await _employeeRepository.UpdateEmployeePhoneAsync(id, phone);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }

        public async Task<EmployeeResponseModel> UpdateEmployeeIsActiveAsync(Guid id, bool isActive)
        {
            var employee = await _employeeRepository.UpdateEmployeeIsActiveAsync(id, isActive);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }
    }
}
