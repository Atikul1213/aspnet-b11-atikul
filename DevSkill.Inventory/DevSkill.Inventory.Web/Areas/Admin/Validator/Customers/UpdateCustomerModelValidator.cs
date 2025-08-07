using DevSkill.Inventory.Web.Areas.Admin.Models.Customers;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Customers
{
    public class UpdateCustomerModelValidator : AbstractValidator<UpdateCustomerModel>
    {
        public UpdateCustomerModelValidator()
        {
            RuleFor(p => p.MobileNumber)
                .NotEmpty().WithMessage("Customer mobile number is required.")
                .Matches(@"^(?:\+?88)?01[3-9]\d{8}$").WithMessage("Please enter a valid Bangladeshi mobile number.");
        }
    }
}
