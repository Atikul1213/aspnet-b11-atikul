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


            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("Product Sku is required.")
                .Length(3, 15).WithMessage("Product Sku must be between 3 and 15 characters long.");


            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than 0.")
                .LessThan(1000000).WithMessage("Product price must be less than 1,000,000.");

            RuleFor(p => p.Quantity)
                .LessThan(1000).WithMessage("Product quantity must be less than 1000.");

            RuleFor(p => p.Id)
                .Must(id => id != Guid.Empty).WithMessage("Product Id must be a valid GUID.");
        }
    }
}
