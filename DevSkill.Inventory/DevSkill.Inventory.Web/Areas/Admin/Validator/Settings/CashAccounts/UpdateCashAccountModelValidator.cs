using DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.CashAccounts
{
    public class UpdateCashAccountModelValidator : AbstractValidator<UpdateCashAccountModel>
    {
        public UpdateCashAccountModelValidator()
        {
            RuleFor(c => c.Name)
              .NotEmpty().WithMessage("CashAccount name is required.");
        }
    }
}
