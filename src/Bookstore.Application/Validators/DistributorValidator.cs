using Bookstore.DataAccess.Entities;
using FluentValidation;

namespace Bookstore.Application.Validators
{
    public class DistributorValidator : AbstractValidator<Distributor>
    {
        public DistributorValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Continue;
            RulesForName();
            RulesForAddress();
            RulesForPhone();
            RulesForIsActive();
    }

        private void RulesForName()
        {
            RuleFor(dis => dis.Name).NotNull().WithMessage("Name is null");
            RuleFor(dis => dis.Name).NotEmpty().WithMessage("Name is empty");
            RuleFor(dis => dis.Name).Length(1, 50).WithMessage("Name must have a length between 1 and 50 characters.");

        }

        private void RulesForAddress()
        {
            RuleFor(dis => dis.Address).NotNull().WithMessage("Address is null");
            RuleFor(dis => dis.Address).NotEmpty().WithMessage("Address is empty");
            RuleFor(dis => dis.Address).Length(1, 100).WithMessage("Address must have a length between 1 and 100 characters.");
        }

        private void RulesForPhone()
        {
            RuleFor(dis => dis.Phone).NotNull().WithMessage("Phone is null");
            RuleFor(dis => dis.Phone).NotEmpty().WithMessage("Phone is empty");
            RuleFor(dis => dis.Phone).Length(10).WithMessage("Phone must have exactly 10 characters.");
            RuleFor(dis => dis.Phone).Must(ContainNumbersOnly).WithMessage("Phone must contain numbers only.");
        }

        private void RulesForIsActive()
        {
            RuleFor(dis => dis.IsActive).NotNull().WithMessage("IsActive is null");
            RuleFor(dis => dis.IsActive).NotEmpty().WithMessage("IsActive is empty");
        }

        private bool ContainNumbersOnly(string property)
        {
            if (property.All(char.IsNumber)) return true;
            else return false;
        }
    }
}
