using AutoMapper;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Web.Areas.Admin.Models.UserRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRolesController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<UserRolesController> _logger;
        private readonly IMediator _mediator;
        private readonly RoleManager<ApplicationRole> _roleManager;
        #endregion

        #region Ctor
        public UserRolesController(IMapper mapper,
            ILogger<UserRolesController> logger,
            IMediator mediator,
            RoleManager<ApplicationRole> roleManager)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _roleManager = roleManager;
        }
        #endregion

        #region Index AddUserRole UpdateUserRole RemoveUserRole

        [Authorize(Roles = "Admin,Registered")]
        public async Task<IActionResult> Index()
        {
            var userRoles = await _roleManager.Roles.ToListAsync();

            var model = new UserRoleListModel();
            model.AddUserRoleModel.CompanyId = (int)Company.SunshineIt;
            model.AddUserRoleModel.StatusId = (int)Status.Active;

            model.AddUserRoleModel.Companies = EnumHelper.PrepareSelectList<Company>();

            model.UpdateUserRoleModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var userRole in userRoles)
            {
                var userRoleModel = _mapper.Map<UserRoleModel>(userRole);
                //userRoleModel.Status = ((Status)userRole.StatusId).ToString();
                //userRoleModel.Company = ((Company)userRole.CompanyId).ToString();

                model.UserRoles.Add(userRoleModel);
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserRole(AddUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var applicationRole = _mapper.Map<ApplicationRole>(model);
                    applicationRole.NormalizedName = model.Name.ToUpperInvariant();
                    applicationRole.ConcurrencyStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");

                    await _roleManager.CreateAsync(applicationRole);

                    TempData["success"] = "UserRole created successfully.";

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


        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var prevRole = await _roleManager.FindByIdAsync(model.Id.ToString());

                    if (prevRole is not null)
                    {
                        prevRole.Name = model.Name;
                        prevRole.NormalizedName = model.Name.ToUpperInvariant();
                        //prevRole.StatusId = model.StatusId;
                        //prevRole.CompanyId = model.CompanyId;
                        prevRole.ConcurrencyStamp = Guid.NewGuid().ToString();

                        await _roleManager.UpdateAsync(prevRole);

                        TempData["success"] = "UserRole updated successfully.";
                    }

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


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveUserRole(Guid id)
        {
            try
            {
                var userRole = await _roleManager.FindByIdAsync(id.ToString());
                if (userRole == null)
                {
                    TempData["error"] = "UserRole not found.";
                    return RedirectToAction("Index");
                }

                await _roleManager.DeleteAsync(userRole);

                TempData["success"] = "UserRole deleted successfully.";
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
