using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator
{
    public class AddProductModelValidator : AbstractValidator<AddProductModel>
    {
        public AddProductModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 60).WithMessage("Product name must be between 3 and 60 characters long.");

            RuleFor(p => p.CategoryId)
                .NotEqual(Guid.Empty).WithMessage("Category is required.");

            RuleFor(p => p.UnitId)
                .NotEqual(Guid.Empty).WithMessage("Unit is required.");

            RuleFor(p => p.BarCode)
                .NotEmpty().WithMessage("Product bar code is required.")
                .Length(3, 65).WithMessage("Product bar code must be between 3 and 65 characters long.");

        }
    }
}
