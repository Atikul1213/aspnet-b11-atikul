using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Department
{
    public class AddDepartmentModelValidator : AbstractValidator<AddDepartmentModel>
    {
        public AddDepartmentModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Department name is required.");
        }
    }
}
