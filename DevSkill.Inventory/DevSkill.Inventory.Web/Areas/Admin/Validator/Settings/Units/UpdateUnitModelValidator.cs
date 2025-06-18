using DevSkill.Inventory.Web.Areas.Admin.Models.Unit;
using FluentValidation;

namespace DevSkill.Inventory.Web.Areas.Admin.Validator.Settings.Units
{
    public class UpdateUnitModelValidator : AbstractValidator<UpdateUnitModel>
    {
        public UpdateUnitModelValidator()
        {
            RuleFor(c => c.Name)
             .NotEmpty().WithMessage("Unit name is required.");
        }
    }
}
