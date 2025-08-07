using DevSkill.Inventory.Web.Areas.Admin.Models.UserModel;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.UserRoles
{
    public class AddUserRoleModelValidator : AbstractValidator<AddUserRoleModel>
    {
        public AddUserRoleModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("User role name is required.");
        }
    }
}
