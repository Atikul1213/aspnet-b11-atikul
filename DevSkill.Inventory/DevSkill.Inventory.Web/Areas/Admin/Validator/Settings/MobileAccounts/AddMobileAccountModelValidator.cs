using DevSkill.Inventory.Web.Areas.Admin.Models.MobileAccount;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.MobileAccounts
{
    public class AddMobileAccountModelValidator : AbstractValidator<AddMobileAccountModel>
    {
        public AddMobileAccountModelValidator()
        {
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("Account name is required.");

            RuleFor(c => c.AccountNo)
              .NotEmpty().WithMessage("Account no is required.");
        }
    }
}
