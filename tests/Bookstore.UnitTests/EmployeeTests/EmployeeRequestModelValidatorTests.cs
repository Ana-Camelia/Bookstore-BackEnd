using Bookstore.Application.Models.Employee;
using Bookstore.Application.Validators;
using FluentValidation.TestHelper;
using NSubstitute;

namespace Bookstore.UnitTests.EmployeeTests
{
    public class EmployeeRequestModelValidatorTests
    {
        private readonly EmployeeRequestModelValidator _requestModelValidator;

        public EmployeeRequestModelValidatorTests()
        {
            _requestModelValidator = new EmployeeRequestModelValidator();
        }

        [Fact]
        public void Should_Throw_Exception_For_FirstName_Length()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                CNP = "",
                Role = "",
                Phone = "",
                Password = "",
                IsActive = true
            };
            
            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.FirstName)
                .WithErrorMessage("First name must have a length between 1 and 30 characters.");
        }

        [Fact]
        public void Should_Throw_Exception_For_FirstName_Must_ContainLettersOnly()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "@",
                LastName = "",
                CNP = "",
                Role = "",
                Phone = "",
                Password = "",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.FirstName)
                .WithErrorMessage("First name must contain letters only.");
        }

        [Fact]
        public void Should_Throw_Exception_For_LastName_Length()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                CNP = "",
                Role = "",
                Phone = "",
                Password = "",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.LastName)
                .WithErrorMessage("Last name must have a length between 1 and 30 characters.");
        }

        [Fact]
        public void Should_Throw_Exception_For_LastName_Must_ContainLettersOnly()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa1aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                CNP = "",
                Role = "",
                Phone = "",
                Password = "",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.LastName)
                .WithErrorMessage("Last name must contain letters only.");
        }

        [Fact]
        public void Should_Throw_Exception_For_Cnp()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                CNP = "cnp",
                Role = "",
                Phone = "",
                Password = "",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.CNP)
                .WithErrorMessage("CNP must have exactly 13 characters.");

            result.ShouldHaveValidationErrorFor(emp => emp.CNP)
                .WithErrorMessage("CNP must contain numbers only.");

            result.ShouldHaveValidationErrorFor(emp => emp.CNP)
                .WithErrorMessage("CNP must have the valid format.");
        }

        [Fact]
        public void Should_Throw_Exception_For_Role_Must_ContainLettersOnly()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                CNP = "",
                Role = "1",
                Phone = "",
                Password = "",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.Role)
                .WithErrorMessage("Role must contain letters only.");
        }

        [Fact]
        public void Should_Throw_Exception_For_Phone()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                CNP = "",
                Role = "",
                Phone = "phone",
                Password = "",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.Phone)
                .WithErrorMessage("Phone must have exactly 10 characters.");

            result.ShouldHaveValidationErrorFor(emp => emp.Phone)
                .WithErrorMessage("Phone must contain numbers only.");
        }

        [Fact]
        public void Should_Throw_Exception_For_Password()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                CNP = "",
                Role = "",
                Phone = "",
                Password = "pass",
                IsActive = true
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.Password)
                .WithErrorMessage("Password must have a length between 8 and 30 characters.");

            result.ShouldHaveValidationErrorFor(emp => emp.Password)
                .WithErrorMessage("Password must contain at least 1 letter and at least 1 number.");
        }

        [Fact]
        public void Should_Throw_Exception_For_IsActive_IsEmpty()
        {
            var employee = new EmployeeRequestModel()
            {
                FirstName = "",
                LastName = "",
                CNP = "",
                Role = "",
                Phone = "",
                Password = "pass"
            };

            var result = _requestModelValidator.TestValidate(employee);

            result.ShouldHaveValidationErrorFor(emp => emp.IsActive)
                .WithErrorMessage("IsActive is empty");
        }
    }
}
