using DevSkill.Inventory.Application.Features.Customers.Queries;
using DevSkill.Inventory.Application.Features.Users.Employees.Queries;
using DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Queries;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Utilities;
using DevSkill.Inventory.Web.Areas.Admin.Models.UserModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        #region Fields

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<UsersController> _logger;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IMediator _mediator;

        #endregion


        #region Ctor
        public UsersController(UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            ILogger<UsersController> logger,
            RoleManager<ApplicationRole> roleManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = IdentityHelper.GetEmailStore(userManager, userStore);
            _logger = logger;
            _roleManager = roleManager;
            _mediator = mediator;
        }

        #endregion

        #region AddUser AddUserRole  UserCategory
        public IActionResult AddUser()
        {
            var model = new AddUserModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserAsync(AddUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = IdentityHelper.CreateUser();

                    await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

                    var result = await _userManager.CreateAsync(user, model.Password);
                    await _userManager.AddToRoleAsync(user, model.Role);

                    TempData["success"] = "User created successfully.";
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    TempData["error"] = "Failed to create user.";
                }
            }
            return View(model);
        }

        public async Task<IActionResult> AddUserRoleAsync()
        {
            var model = new AddUserRoleModel();
            model = await PrepareAddUserRoleModelAsync(model);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserRoleAsync(AddUserRoleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(model.UserId.ToString());

                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                        TempData["error"] = "User not found with the id";
                        model = await PrepareAddUserRoleModelAsync(model);

                        return View(model);
                    }

                    var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());

                    if (role == null)
                    {
                        ModelState.AddModelError(string.Empty, "Role not found.");
                        TempData["error"] = "Role not found with the id";
                        model = await PrepareAddUserRoleModelAsync(model);

                        return View(model);
                    }

                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        ModelState.AddModelError(string.Empty, "User is already assigned to this role.");
                        TempData["error"] = "User already has this role.";
                        model = await PrepareAddUserRoleModelAsync(model);

                        return View(model);
                    }

                    var result = await _userManager.AddToRoleAsync(user, role.Name);
                    if (result.Succeeded)
                    {
                        TempData["success"] = "User role created successfully.";

                        return RedirectToAction("Index", "Home");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    TempData["error"] = "Failed to assign user role.";

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    TempData["error"] = "Failed to create user role.";
                    _logger.LogError(ex, "Error occurred while adding user role.");
                }
            }

            model = await PrepareAddUserRoleModelAsync(model);

            return View(model);
        }

        private async Task<AddUserRoleModel> PrepareAddUserRoleModelAsync(AddUserRoleModel model)
        {
            var users = await _userManager.Users.ToListAsync();
            model.Users = users.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.FirstName} {u.LastName}"
            });

            var roles = await _roleManager.Roles.ToListAsync();

            model.Roles = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            });

            return model;
        }

        public async Task<IActionResult> UserCategory()
        {
            var model = new UserCategoryModel();

            var suppliers = await _mediator.Send(new GetAllSuppliersQuery());
            model.SupplierCount = suppliers.Count;

            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            model.EmployeeCount = employees.Count;

            var inventoryUsers = await _mediator.Send(new GetAllInventoryUsersQuery());
            model.UserCount = inventoryUsers.Count;

            var customers = await _mediator.Send(new GetAllCustomersQuery());
            model.CustomerCount = customers.Count;

            return View(model);
        }

        #endregion
    }
}
