using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
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

            var command = new GetCategoryListQuery();
            var categories = await _mediator.Send(command);
            model.CategoryCount = categories.Count;

            return View(model);
        }
        #endregion
    }
}
