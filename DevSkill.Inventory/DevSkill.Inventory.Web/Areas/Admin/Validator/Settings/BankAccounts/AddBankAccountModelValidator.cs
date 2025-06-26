using DevSkill.Inventory.Web.Areas.Admin.Models.BankAccounts;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.BankAccounts
{
    public class AddBankAccountModelValidator : AbstractValidator<AddBankAccountModel>
    {
        public AddBankAccountModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("Account name is required.");

            RuleFor(c => c.AccountNo)
              .NotEmpty().WithMessage("Account no is required.");

            RuleFor(c => c.BankName)
              .NotEmpty().WithMessage("Bank name is required.");
        }
    }
}
