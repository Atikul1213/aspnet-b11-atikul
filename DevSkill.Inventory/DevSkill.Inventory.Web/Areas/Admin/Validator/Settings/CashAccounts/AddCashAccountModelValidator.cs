using DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.CashAccounts
{
    public class AddCashAccountModelValidator : AbstractValidator<AddCashAccountModel>
    {
        public AddCashAccountModelValidator()
        {
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage("CashAccount name is required.");
        }
    }
}
