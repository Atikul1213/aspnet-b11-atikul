using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Department
{
    public class UpdateDepartmentModelValidator : AbstractValidator<UpdateDepartmentModel>
    {
        public UpdateDepartmentModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Department name is required.");
        }
    }
}
