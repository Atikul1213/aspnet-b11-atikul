using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Commands;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.UserRole;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRolesController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<UserRolesController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public UserRolesController(IMapper mapper,
            ILogger<UserRolesController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddUserRole UpdateUserRole RemoveUserRole
        public async Task<IActionResult> Index()
        {
            var getUserRoleListQuery = new GetUserRoleListQuery();
            var userRoles = await _mediator.Send(getUserRoleListQuery);

            var model = new UserRoleListModel();
            model.AddUserRoleModel.CompanyId = (int)Company.SunshineIt;
            model.AddUserRoleModel.StatusId = (int)Status.Active;

            model.AddUserRoleModel.Companies = EnumHelper.PrepareSelectList<Company>();

            model.UpdateUserRoleModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var userRole in userRoles)
            {
                var userRoleModel = _mapper.Map<UserRoleModel>(userRole);
                userRoleModel.Status = ((Status)userRole.StatusId).ToString();
                userRoleModel.Company = ((Company)userRole.CompanyId).ToString();

                model.UserRoles.Add(userRoleModel);
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserRole(AddUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userRole = _mapper.Map<UserRoleAddCommand>(model);
                    await _mediator.Send(userRole);

                    TempData["success'"] = "UserRole created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create userRole");
                }
            }
            TempData["error"] = "Failed to create userRole.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userRole = _mapper.Map<UpdateUserRoleCommand>(model);
                    await _mediator.Send(userRole);
                    TempData["success'"] = "UserRole updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update userRole");
                }
            }
            TempData["error"] = "Failed to update userRole.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveUserRole(Guid id)
        {
            try
            {
                var userRole = new UserRoleDeleteCommand(id);
                await _mediator.Send(userRole);

                TempData["success'"] = "UserRole deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete userRole");
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
