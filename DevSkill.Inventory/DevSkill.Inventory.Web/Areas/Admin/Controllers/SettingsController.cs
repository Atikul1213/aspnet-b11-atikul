using DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries;
using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
using DevSkill.Inventory.Application.Features.Settings.Departments.Queries;
using DevSkill.Inventory.Application.Features.Settings.Units.Queries;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries;
using DevSkill.Inventory.Web.Areas.Admin.Models.Setting;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        #region Fields
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index()
        {
            var model = new SettingModel();

            var categories = await _mediator.Send(new GetCategoryListQuery());
            model.CategoryCount = categories.Count;

            var units = await _mediator.Send(new GetUnitListQuery());
            model.UnitCount = units.Count;

            var departments = await _mediator.Send(new GetDepartmentListQuery());
            model.DepartmentCount = departments.Count;

            var userRoles = await _mediator.Send(new GetUserRoleListQuery());
            model.UserRoleCount = userRoles.Count;

            var cashAccounts = await _mediator.Send(new GetCashAccountListQuery());
            model.CashAccountCount = cashAccounts.Count;

            var bankAccounts = await _mediator.Send(new GetBankAccountListQuery());
            model.BankAccountCount = bankAccounts.Count;

            return View(model);
        }
        #endregion
    }
}
