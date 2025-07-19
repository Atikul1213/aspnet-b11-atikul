using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.Units.Commands;
using DevSkill.Inventory.Application.Features.Settings.Units.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Unit;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitsController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<UnitsController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public UnitsController(IMapper mapper,
            ILogger<UnitsController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddUnit UpdateUnit RemoveUnit

        [Authorize(Roles = "Admin,Registered")]
        public async Task<IActionResult> Index()
        {
            var getUnitListQuery = new GetUnitListQuery();
            var units = await _mediator.Send(getUnitListQuery);

            var model = new UnitListModel();
            model.AddUnitModel.StatusId = (int)Status.Active;
            model.AddUnitModel.Status = EnumHelper.PrepareSelectList<Status>();

            model.UpdateUnitModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var unit in units)
            {
                var unitModel = _mapper.Map<UnitModel>(unit);
                unitModel.Status = ((Status)unit.StatusId).ToString();
                unitModel.CreateOnUtc = unit.CreateOnUtc.ToString("dd-MM-yyyy");

                model.Units.Add(unitModel);
            }

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUnit(AddUnitModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var unit = _mapper.Map<UnitAddCommand>(model);
                    await _mediator.Send(unit);

                    TempData["success"] = "Unit created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create unit");
                }
            }
            TempData["error"] = "Failed to create unit.";

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUnit(UpdateUnitModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var unit = _mapper.Map<UpdateUnitCommand>(model);
                    await _mediator.Send(unit);
                    TempData["success"] = "Unit updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update unit");
                }
            }
            TempData["error"] = "Failed to update unit.";

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUnit(Guid id)
        {
            try
            {
                var unit = new UnitDeleteCommand(id);
                await _mediator.Send(unit);

                TempData["success"] = "Unit deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete unit");
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
