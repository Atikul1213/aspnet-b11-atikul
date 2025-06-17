using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Categories
{
    public class UpdateCategoryModelValidator : AbstractValidator<UpdateCategoryModel>
    {
        public UpdateCategoryModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.");
        }
    }
}
