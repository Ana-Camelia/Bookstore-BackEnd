using Bookstore.Application.Models.Employee;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Bookstore.Application.Validators
{
    public class EmployeeRequestModelValidator : AbstractValidator<EmployeeRequestModel>
    {
        public EmployeeRequestModelValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RulesForFirstName();
            RulesForLastName();
            RulesForCNP();
            RulesForRole();
            RulesForPhone();
            RulesForPassword();
            RulesForIsActive();
        }

        private void RulesForFirstName()
        {
            RuleFor(emp => emp.FirstName).NotNull().WithMessage("First name is null");
            RuleFor(emp => emp.FirstName).NotEmpty().WithMessage("First name is empty");
            RuleFor(emp => emp.FirstName).Length(1, 30).WithMessage("First Name must have a length between 1 and 30 characters.");
            RuleFor(emp => emp.FirstName).Must(ContainLettersOnly).WithMessage("First name must contain letters only.");
        }

        private void RulesForLastName()
        {
            RuleFor(emp => emp.LastName).NotNull().WithMessage("Last name is null");
            RuleFor(emp => emp.LastName).NotEmpty().WithMessage("Last name is empty");
            RuleFor(emp => emp.LastName).Length(1, 30).WithMessage("Last Name must have a length between 1 and 30 characters.");
            RuleFor(emp => emp.LastName).Must(ContainLettersOnly).WithMessage("Last name must contain letters only.");
        }

        private void RulesForCNP()
        {
            RuleFor(emp => emp.CNP).NotNull().WithMessage("CNP is null");
            RuleFor(emp => emp.CNP).NotEmpty().WithMessage("CNP is empty");
            RuleFor(emp => emp.CNP).Length(13).WithMessage("CNP must have exactly 13 characters.");
            RuleFor(emp => emp.CNP).Must(ContainNumbersOnly).WithMessage("CNP must contain numbers only.");
            RuleFor(emp => emp.CNP).Must(HaveValidFormat).WithMessage("CNP must have the valid format.");
        }

        private void RulesForRole()
        {
            RuleFor(emp => emp.Role).NotNull().WithMessage("Role is null");
            RuleFor(emp => emp.Role).NotEmpty().WithMessage("Role is empty");
            RuleFor(emp => emp.Role).Must(ContainLettersOnly).WithMessage("Role must contain letters only.");
        }

        private void RulesForPhone()
        {
            RuleFor(emp => emp.Phone).NotNull().WithMessage("Phone is null");
            RuleFor(emp => emp.Phone).NotEmpty().WithMessage("Phone is empty");
            RuleFor(emp => emp.Phone).Length(10).WithMessage("Phone must have exactly 10 characters.");
            RuleFor(emp => emp.Phone).Must(ContainNumbersOnly).WithMessage("Phone must contain numbers only.");
        }

        private void RulesForPassword()
        {
            RuleFor(emp => emp.Password).NotNull().WithMessage("Password is null");
            RuleFor(emp => emp.Password).NotEmpty().WithMessage("Password is empty");
            RuleFor(emp => emp.Password).Length(8, 30).WithMessage("Password must have a length between 8 and 30 characters.");
            RuleFor(emp => emp.Phone).Must(ContainNumbersAndLetters).WithMessage("Phone must contain at least 1 letter and at least 1 number.");
        }

        private void RulesForIsActive()
        {
            RuleFor(emp => emp.IsActive).NotNull().WithMessage("IsActive is null");
            RuleFor(emp => emp.IsActive).NotEmpty().WithMessage("IsActive is empty");
        }

        private bool ContainLettersOnly(string property)
        {
            if(property.All(char.IsLetter)) return true;
            else return false;
        }

        private bool ContainNumbersOnly(string property)
        {
            if (property.All(char.IsNumber)) return true;
            else return false;
        }

        private bool ContainNumbersAndLetters(string property)
        {
            if (property.Any(char.IsLetter) && property.Any(char.IsNumber)) return true;
            else return false;
        }

        private bool HaveValidFormat(string cnp)
        {
            string regex = @"^[1256]\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])(0[1-9]|[1-4]\d|5[012])(00[1-9]|0[1-9]\d|[1-9]\d\d)\d$";
            return Regex.IsMatch(cnp, regex);
        }
    }
}
