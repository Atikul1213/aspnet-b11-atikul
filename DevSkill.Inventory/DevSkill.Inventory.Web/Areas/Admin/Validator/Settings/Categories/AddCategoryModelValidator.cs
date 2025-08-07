using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Categories
{
    public class AddCategoryModelValidator : AbstractValidator<AddCategoryModel>
    {
        public AddCategoryModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.");
        }
    }
}
