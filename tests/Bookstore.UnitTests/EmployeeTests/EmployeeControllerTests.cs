using Bookstore.API;
using Bookstore.API.Controllers;
using Bookstore.Application.Models.Employee;
using Bookstore.Application.Services;
using Bookstore.DataAccess.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;

namespace Bookstore.UnitTests.EmployeeTests
{
    public class EmployeeControllerTests
    {
        private readonly EmployeesController _controller;
        private readonly IEmployeeService _service;

        public EmployeeControllerTests()
        {
            _service = Substitute.For<IEmployeeService>();
            _controller = new EmployeesController(_service);
        }

        [Fact]
        public async void GetEmployeesAsync_Should_Return_A_List()
        {
            _service.GetEmployeesAsync().Returns(new List<EmployeeResponseModel>());

            var empList = await _controller.GetEmployeesAsync() as ObjectResult;
            var result = empList.Value as ApiResponse<List<EmployeeResponseModel>>;
            
            result.Succeeded.Should().BeTrue();
            result.Result.Should().BeOfType<List<EmployeeResponseModel>>();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void GetEmployeeByIdAsync_Should_Return_A_Valid_Employee()
        {
            _service.GetEmployeeByIdAsync(Arg.Any<Guid>()).Returns(new EmployeeResponseModel());

            var emp = await _controller.GetEmployeeByIdAsync(new Guid()) as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;
            
            result.Succeeded.Should().BeTrue();
            result.Result.Should().BeOfType<EmployeeResponseModel>();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void GetEmployeeByCnpAsync_Should_Not_Return_Anything()
        {
            _service.GetEmployeeByCnpAsync(Arg.Any<string>()).ReturnsNull();

            var emp = await _controller.GetEmployeeByCnpAsync("cnp") as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;
            
            result.Succeeded.Should().BeTrue();
            result.Result.Should().BeNull();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void CreateEmployeeAsync_Should_Return_A_Valid_Employee()
        {
            _service.CreateEmployeeAsync(Arg.Any<EmployeeRequestModel>())
                .Returns(new EmployeeResponseModel());

            var emp = await _controller.CreateEmployeeAsync(new EmployeeRequestModel()) as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;

            result.Succeeded.Should().BeTrue();
            result.Result.Should().BeOfType<EmployeeResponseModel>();
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void CreateEmployeeAsync_Should_Return_Conflict_Cause_By_Id()
        {
            _service.CreateEmployeeAsync(Arg.Any<EmployeeRequestModel>())
                .Throws<EmployeeAlreadyExistsException>();

            var emp = await _controller.CreateEmployeeAsync(new EmployeeRequestModel()) as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;

            result.Succeeded.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Errors.Should().HaveCount(1);
            result.Errors.ElementAt(0).Field.Should().Be("Id");
        }

        [Fact]
        public async void CreateEmployeeAsync_Should_Return_Conflict_Cause_By_Role()
        {
            _service.CreateEmployeeAsync(Arg.Any<EmployeeRequestModel>())
                .Throws<RoleNotFoundException>();

            var emp = await _controller.CreateEmployeeAsync(new EmployeeRequestModel()) as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;

            result.Succeeded.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Errors.Should().HaveCount(1);
            result.Errors.ElementAt(0).Field.Should().Be("Role");
        }

        [Fact]
        public async void UpdateEmployeePhoneAsync_Should_Return_A_Valid_Employee()
        {
            string phone = "075";
            _service.UpdateEmployeePhoneAsync(Arg.Any<Guid>(), phone)
                .Returns(new EmployeeResponseModel() { Phone = phone });

            var emp = await _controller.UpdateEmployeePhoneAsync(new Guid(), phone) as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;

            result.Succeeded.Should().BeTrue();
            result.Result.Should().BeOfType<EmployeeResponseModel>();
            result.Result.Phone.Should().Be(phone);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void UpdateEmployeeIsActiveAsync_Should_Return_A_Valid_Employee()
        {
            bool isActive = true;
            _service.UpdateEmployeeIsActiveAsync(Arg.Any<Guid>(), isActive)
                .Throws<EmployeeNotFoundException>();

            var emp = await _controller.UpdateEmployeeIsActiveAsync(new Guid(), isActive) as ObjectResult;
            var result = emp.Value as ApiResponse<EmployeeResponseModel>;

            result.Succeeded.Should().BeFalse();
            result.Result.Should().BeNull();
            result.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async void DeleteEmployeeByIdAsync_Should_Return_Success_Message()
        {
            string successMessage = "Action completed successfully";

            var emp = await _controller.DeleteEmployeeByIdAsync(new Guid()) as ObjectResult;
            var result = emp.Value as ApiResponse<string>;

            result.Succeeded.Should().BeTrue();
            result.Result.Should().Be(successMessage);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public async void DeleteEmployeeByIdAsync_Should_Return_NotFound_Message()
        {
            string failMessage = "This employee does not exist.";
            _service.DeleteEmployeeByIdAsync(Arg.Any<Guid>())
                .Returns(Task.FromException(new EmployeeNotFoundException(failMessage)));

            var emp = await _controller.DeleteEmployeeByIdAsync(new Guid()) as ObjectResult;
            var result = emp.Value as ApiResponse<string>;

            result.Succeeded.Should().BeFalse();
            result.Errors.Should().HaveCount(1);
            result.Errors.ElementAt(0).Field.Should().BeNull();
            result.Errors.ElementAt(0).Message.Should().Be(failMessage);
        }

    }
}
