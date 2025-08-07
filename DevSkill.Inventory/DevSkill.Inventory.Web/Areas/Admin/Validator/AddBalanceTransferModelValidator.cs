using DevSkill.Inventory.Web.Areas.Admin.Models.BalanceTransfers;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator
{
    public class AddBalanceTransferModelValidator : AbstractValidator<AddBalanceTransferModel>
    {
        public AddBalanceTransferModelValidator()
        {
            RuleFor(c => c.SendingAccountTypeId)
               .GreaterThan(0).WithMessage("Sending account is required.");

            RuleFor(c => c.ReceiveAccountTypeId)
               .GreaterThan(0).WithMessage("Receiving account is required.");

            RuleFor(c => c.SendingAccountId)
                .NotEqual(Guid.Empty).WithMessage("Sending account is required.");

            RuleFor(c => c.ReceiveAccountId)
                .NotEqual(Guid.Empty).WithMessage("Receiving account is required.");
        }
    }
}
