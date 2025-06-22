using DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Users.InventoryUsers
{
    public class AddInventoryUserModelValidator : AbstractValidator<AddInventoryUserModel>
    {
        public AddInventoryUserModelValidator()
        {
            RuleFor(c => c.EmployeeId)
                 .NotEmpty().NotEqual(Guid.Empty)
                 .WithMessage("Employee is required.");

            RuleFor(c => c.UserRoleId)
                  .NotEmpty().NotEqual(Guid.Empty)
                  .WithMessage("Role is required.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
