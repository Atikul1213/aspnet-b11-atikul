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
                .Length(3, 20).WithMessage("Product name must be between 3 and 20 characters long.");


            RuleFor(p => p.Sku)
                .NotEmpty().WithMessage("Product Sku is required.")
                .Length(3, 15).WithMessage("Product Sku must be between 3 and 15 characters long.");


            //RuleFor(p => p.Price)
            //    .LessThan(5000).WithMessage("Product price must be less than 5000.");

            //RuleFor(p => p.Quantity)
            //    .LessThan(100).WithMessage("Product quantity must be less than 100.");

        }
    }
}
