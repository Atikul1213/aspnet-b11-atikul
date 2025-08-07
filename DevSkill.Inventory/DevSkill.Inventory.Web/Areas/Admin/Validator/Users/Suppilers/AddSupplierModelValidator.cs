using DevSkill.Inventory.Web.Areas.Admin.Models.Supplier;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Users.Suppilers
{
    public class AddSupplierModelValidator : AbstractValidator<AddSupplierModel>
    {
        public AddSupplierModelValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Supplier name is required.");

            RuleFor(c => c.MobileNumber)
                .NotEmpty().WithMessage("Supplier mobile number is required.");
        }
    }
}
