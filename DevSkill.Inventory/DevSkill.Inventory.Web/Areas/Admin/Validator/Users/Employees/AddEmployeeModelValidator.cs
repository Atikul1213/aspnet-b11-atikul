using DevSkill.Inventory.Web.Areas.Admin.Models.Employees;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Users.Employees
{
    public class AddEmployeeModelValidator : AbstractValidator<AddEmployeeModel>
    {
        public AddEmployeeModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Employee name is required.");

            RuleFor(c => c.Address)
                .NotEmpty().WithMessage("Employee address is required.");

            RuleFor(c => c.MobileNumber)
                .NotEmpty().WithMessage("Contact number is required.");

            RuleFor(c => c.JoiningDate)
                .NotEmpty().WithMessage("Joining date is required.");
        }
    }
}
