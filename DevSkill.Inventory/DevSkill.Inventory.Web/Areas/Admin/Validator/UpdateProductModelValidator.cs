using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator
{
    public class UpdateProductModelValidator : AbstractValidator<UpdateProductModel>
    {
        public UpdateProductModelValidator()
        {
            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("Product name is required.")
             .Length(3, 20).WithMessage("Product name must be between 3 and 20 characters long.");

            RuleFor(p => p.CategoryId)
                .NotEqual(Guid.Empty).WithMessage("Category is required.");

            RuleFor(p => p.UnitId)
                .NotEqual(Guid.Empty).WithMessage("Unit is required.");

            RuleFor(p => p.BarCode)
                .NotEmpty().WithMessage("Product bar code is required.")
                .Length(3, 15).WithMessage("Product bar code must be between 3 and 15 characters long.");


            RuleFor(p => p.Id)
                .Must(id => id != Guid.Empty).WithMessage("Product Id must be a valid GUID.");
        }
    }
}
