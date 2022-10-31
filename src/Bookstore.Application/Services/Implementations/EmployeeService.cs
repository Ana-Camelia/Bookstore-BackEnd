using AutoMapper;
using Bookstore.Application.Exceptions;
using Bookstore.Application.Models.Employee;
using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Exceptions;
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

        public async Task<EmployeeResponseModel> GetEmployeeByCnpAsync(string cnp)
        {
            var employee = await _employeeRepository.GetEmployeeByCnpAsync(cnp);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }

        public async Task<EmployeeResponseModel> CreateEmployeeAsync(EmployeeRequestModel employee)
        {
            var newEmployee = _mapper.Map<Employee>(employee);
            newEmployee.Id = new Guid();
            newEmployee.Role = await _roleRepository.GetRoleByNameAsync(employee.Role);

            var searchResult = await _employeeRepository.GetEmployeeByIdAsync(newEmployee.Id);
            if(searchResult != null)
                throw new EmployeeAlreadyExistsException("An employee with this ID already exists.");

            searchResult = await _employeeRepository.GetEmployeeByCnpAsync(newEmployee.CNP);
            if (searchResult != null)
                throw new CnpAlreadyExistsException("An employee with this CNP already exists.");

            var employeeResponse = await _employeeRepository.CreateEmployeeAsync(newEmployee);

            return _mapper.Map<EmployeeResponseModel>(employeeResponse);
        }

        public async Task<EmployeeResponseModel> UpdateEmployeePhoneAsync(Guid id, string phone)
        {
            var searchResult = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (searchResult == null)
                throw new EmployeeNotFoundException("This employee does not exist.");

            var employee = await _employeeRepository.UpdateEmployeePhoneAsync(searchResult, phone);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }

        public async Task<EmployeeResponseModel> UpdateEmployeeIsActiveAsync(Guid id, bool isActive)
        {
            var searchResult = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (searchResult == null)
                throw new EmployeeNotFoundException("This employee does not exist.");

            var employee = await _employeeRepository.UpdateEmployeeIsActiveAsync(searchResult, isActive);
            return _mapper.Map<EmployeeResponseModel>(employee);
        }

        public async Task DeleteEmployeeByIdAsync(Guid id)
        {
            var searchResult = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (searchResult == null)
                throw new EmployeeNotFoundException("This employee does not exist.");

            await _employeeRepository.DeleteEmployeeByIdAsync(searchResult);
        }
    }
}
