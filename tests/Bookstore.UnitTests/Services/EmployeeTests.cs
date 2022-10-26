using AutoMapper;
using Bookstore.Application.Mapping;
using Bookstore.Application.Models.Employee;
using Bookstore.Application.Services;
using Bookstore.Application.Services.Implementations;
using Bookstore.DataAccess.Entities;
using Bookstore.DataAccess.Exceptions;
using Bookstore.DataAccess.Repositories;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using System;
using System.Net;
using System.Xml.Linq;

namespace Bookstore.UnitTests.Services
{
    public class EmployeeTests
    {
        private readonly IEmployeeService _service;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public EmployeeTests()
        {
            _employeeRepository = Substitute.For<IEmployeeRepository>();
            _roleRepository = Substitute.For<IRoleRepository>();

            var mapperConfiguration = new MapperConfiguration(x =>
            {
                x.AddMaps(typeof(EmployeeProfile));
            });

            _mapper = new Mapper(mapperConfiguration);
            _service = new EmployeeService(_employeeRepository, _roleRepository, _mapper);
        }

        [Fact]
        public async Task GetEmployeesAsync_Should_Return_A_List()
        {
            //Arrange
            _employeeRepository.GetEmployeesAsync().Returns(new List<Employee>());
            //Act
            var result = await _service.GetEmployeesAsync();
            await _employeeRepository.Received(1).GetEmployeesAsync();
            //Assert
            result.Should().BeOfType<List<EmployeeResponseModel>>();
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_Should_Return_A_Valid_Employee()
        {
            //Arrange
            Guid guid = new Guid("3a7053ad-a828-466f-9b78-614ce86104cf");
            var emp = new Employee
            {
                Id = guid,
                FirstName = "Pacocha",
                LastName = "Esta",
                CNP = "2400331054409",
                RoleId = Guid.Parse("dcc6be20-6c76-4f4e-8010-c6198d26ca7e"),
                Phone = "1111111111",
                Password = "kwl20",
                IsActive = true
            };
            _employeeRepository.GetEmployeeByIdAsync(Arg.Any<Guid>())
                .Returns(emp);
            //Act
            var result = await _service.GetEmployeeByIdAsync(guid);
            await _employeeRepository.Received(1).GetEmployeeByIdAsync(guid);
            //Assert
            var empRes = _mapper.Map<EmployeeResponseModel>(emp);
            result.Should().BeEquivalentTo(empRes);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_Should_Be_Null()
        {
            //Arrange
            Guid guid = new Guid("3a7053ad-a828-466f-9b78-614ce86104cf");
            _employeeRepository.GetEmployeeByIdAsync(guid)
                .ReturnsNull();
            //Act
            var result = await _service.GetEmployeeByIdAsync(guid);
            await _employeeRepository.Received(1).GetEmployeeByIdAsync(guid);
            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateEmployeeAsync_Should_Return_New_Employee()
        {
            Guid guid = new Guid();
            var role = new Role
            {
                Id = Guid.Parse("c5e45b7d-d3e9-4097-b933-5c7e4eb858fb"),
                Name = "Admin"
            };
            var empReq = new EmployeeRequestModel
            {
                FirstName = "Pacocha",
                LastName = "Esta",
                CNP = "2400331054409",
                Role = "Admin",
                Phone = "1111111111",
                Password = "kwl20",
                IsActive = true
            };
            var emp = _mapper.Map<Employee>(empReq);
            emp.Id = guid;
            emp.Role = role;

            _roleRepository.GetRoleByNameAsync("Admin").Returns(role);
            _employeeRepository.GetEmployeeByIdAsync(emp.Id).ReturnsNull();
            _employeeRepository.CreateEmployeeAsync(Arg.Any<Employee>()).Returns(emp);

            var result = await _service.CreateEmployeeAsync(empReq);

            await _roleRepository.Received(1).GetRoleByNameAsync("Admin");
            await _employeeRepository.Received(1).GetEmployeeByIdAsync(emp.Id);
            await _employeeRepository.Received(1).CreateEmployeeAsync(Arg.Any<Employee>());

            result.Should().BeOfType<EmployeeResponseModel>();
            result.FirstName.Should().Be("Pacocha");
            result.Role.Should().Be("Admin");
        }

        [Fact]
        public async Task UpdateEmployeePhoneAsync_Should_Return_Updated_Employee_Phone()
        {
            Guid guid = Guid.Parse("0fa2686a-6bb6-40a7-8d8b-13086fef27c6");
            string phone = "1111111111";

            var emp = new Employee
            {
                Id = guid,
                Phone = "4353454453"
            };

            _employeeRepository.GetEmployeeByIdAsync(guid).Returns(emp);
            _employeeRepository.UpdateEmployeePhoneAsync(emp, phone).Returns(new Employee { Id = guid, Phone = phone });

            var result = await _service.UpdateEmployeePhoneAsync(guid, phone);

            await _employeeRepository.Received(1).UpdateEmployeePhoneAsync(emp, phone);

            result.Phone.Should().Be(phone);
        }

        [Fact]
        public async Task UpdateEmployeeIsActiveAsync_Should_Return_Updated_Employee_IsActive()
        {
            Guid guid = Guid.Parse("0fa2686a-6bb6-40a7-8d8b-13086fef27c6");
            bool isActive = false;

            var emp = new Employee
            {
                Id = guid,
                Phone = "4353454453"
            };

            _employeeRepository.GetEmployeeByIdAsync(guid).Returns(emp);
            _employeeRepository.UpdateEmployeeIsActiveAsync(emp, isActive).Returns(new Employee { Id = guid, IsActive = isActive });

            var result = await _service.UpdateEmployeeIsActiveAsync(guid, isActive);

            await _employeeRepository.Received(1).UpdateEmployeeIsActiveAsync(emp, isActive);

            result.IsActive.Should().Be(false);
        }
    }
}
