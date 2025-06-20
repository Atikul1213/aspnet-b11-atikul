using DevSkill.Inventory.Web.Areas.Admin.Models.UserRole;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.UserRoles
{
    public class UpdateUserRoleModelValidator : AbstractValidator<UpdateUserRoleModel>
    {
        public UpdateUserRoleModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("User role name is required.");
        }
    }
}
