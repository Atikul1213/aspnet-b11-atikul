using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
using DevSkill.Inventory.Application.Features.Settings.Departments.Queries;
using DevSkill.Inventory.Application.Features.Settings.Units.Queries;
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

            var categoryQuery = new GetCategoryListQuery();
            var categories = await _mediator.Send(categoryQuery);
            model.CategoryCount = categories.Count;

            var unitQuery = new GetUnitListQuery();
            var units = await _mediator.Send(unitQuery);
            model.UnitCount = units.Count;

            var departmentQuery = new GetDepartmentListQuery();
            var departments = await _mediator.Send(departmentQuery);
            model.DepartmentCount = departments.Count;

            return View(model);
        }
        #endregion
    }
}
