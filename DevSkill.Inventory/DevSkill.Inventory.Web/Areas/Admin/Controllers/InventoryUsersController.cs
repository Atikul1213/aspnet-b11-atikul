using AutoMapper;
using DevSkill.Inventory.Application.Features.Users.Employees.Queries;
using DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands;
using DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Infrastructure.Utilities;
using DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers;
using DevSkill.Inventory.Web.Models.IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Encodings.Web;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InventoryUsersController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryUsersController> _logger;
        private readonly IMediator _mediator;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly IEmailUtility _emailUtility;
        private readonly RoleManager<ApplicationRole> _roleManager;
        #endregion

        #region Ctor
        public InventoryUsersController(IMapper mapper,
            ILogger<InventoryUsersController> logger,
            IMediator mediator,
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            IEmailUtility emailUtility,
            RoleManager<ApplicationRole> roleManager)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = IdentityHelper.GetEmailStore(userManager, userStore);
            _signInManager = signInManager;
            _emailUtility = emailUtility;
            _roleManager = roleManager;
        }
        #endregion

        #region Index AddInventoryUser UpdateInventoryUser RemoveInventoryUser

        [Authorize(Roles = "Admin,Registered")]
        public async Task<IActionResult> Index()
        {
            var model = new InventoryUserListModel();

            var roles = await _roleManager.Roles.ToListAsync();

            var userRoleSelecttList = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToList();

            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            var employeeSelecttList = EnumHelper.PrepareSelectListFromEntities(employees, d => d.Id, d => d.Name);

            model.AddInventoryUserModel.StatusId = (int)Status.Active;
            model.AddInventoryUserModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.AddInventoryUserModel.Employees = employeeSelecttList;
            model.AddInventoryUserModel.UserRoles = userRoleSelecttList;

            model.UpdateInventoryUserModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.UpdateInventoryUserModel.Employees = employeeSelecttList;
            model.UpdateInventoryUserModel.UserRoles = userRoleSelecttList;

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInventoryUser(AddInventoryUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var inventoryUser = _mapper.Map<InventoryUserAddCommand>(model);
                    var employee = await _mediator.Send(new GetEmployeeByIdQuery(model.EmployeeId));

                    var userRole = await _roleManager.FindByIdAsync(model.UserRoleId.ToString());

                    var prevInventoryUsers = await _mediator.Send(new GetInventoryUserByEmailQuery(employee.Email));

                    var isExistRole = prevInventoryUsers.Where(x => x.Role.Contains(userRole.Name, StringComparison.InvariantCultureIgnoreCase)).Any();

                    if (prevInventoryUsers.Count == 0 || !isExistRole)
                    {
                        inventoryUser.EmployeeName = employee.Name;
                        //inventoryUser.Company = userRole != null ? ((Company)userRole.CompanyId).ToString() : Company.BrainStation.ToString();
                        inventoryUser.Email = employee.Email;
                        inventoryUser.MobileNumber = employee.MobileNumber;
                        inventoryUser.Role = userRole?.Name ?? string.Empty;

                        await _mediator.Send(inventoryUser);
                    }

                    var registerModel = new RegisterModel
                    {
                        FirstName = employee.Name,
                        LastName = employee.Name,
                        Email = employee.Email,
                        Password = model.Password,
                        ConfirmPassword = model.Password,
                        PhoneNumber = employee.MobileNumber,
                        DateOfBirth = DateTime.UtcNow.AddYears(-18),

                    };

                    await RegisterUserAsync(registerModel, model.UserRoleId);

                    TempData["success"] = "InventoryUser created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create inventoryUser");
                }
            }
            TempData["error"] = "Failed to create inventoryUser.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateInventoryUser(UpdateInventoryUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var inventoryUser = _mapper.Map<InventoryUserUpdateCommand>(model);
                    var employee = await _mediator.Send(new GetEmployeeByIdQuery(model.EmployeeId));
                    //var userRole = await _mediator.Send(new GetUserRoleByIdQuery(model.UserRoleId));

                    var userRole = await _roleManager.FindByIdAsync(model.UserRoleId.ToString());
                    var prevInventoryUser = await _mediator.Send(new GetInventoryUserByEmailQuery(employee.Email));

                    inventoryUser.EmployeeName = employee.Name;
                    //inventoryUser.Company = ((Company)userRole.CompanyId).ToString();
                    inventoryUser.Email = employee.Email;
                    inventoryUser.MobileNumber = employee.MobileNumber;
                    inventoryUser.Role = userRole.Name;

                    await _mediator.Send(inventoryUser);

                    var registerModel = new RegisterModel
                    {
                        FirstName = employee.Name,
                        LastName = employee.Name,
                        Email = employee.Email,
                        Password = model.Password,
                        ConfirmPassword = model.Password,
                        PhoneNumber = employee.MobileNumber,
                        DateOfBirth = DateTime.UtcNow.AddYears(-18),

                    };

                    await RegisterUserAsync(registerModel, model.UserRoleId);
                    TempData["success"] = "InventoryUser updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update inventoryUser");
                }
            }
            TempData["error"] = "Failed to update inventoryUser.";

            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveInventoryUser(Guid id)
        {
            try
            {
                var inventoryUser = new InventoryUserDeleteCommand(id);
                await _mediator.Send(inventoryUser);

                TempData["success"] = "InventoryUser deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete inventoryUser");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSInventoryUserJsonData([FromBody] GetInventoryUserListQuery model)
        {
            try
            {
                var result = await _mediator.Send(model);

                var inventoryUsers = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.EmployeeName),
                                HttpUtility.HtmlEncode(record.Company),
                                HttpUtility.HtmlEncode(record.Email),
                                HttpUtility.HtmlEncode(record.MobileNumber),
                                HttpUtility.HtmlEncode(record.Role),
                                HttpUtility.HtmlEncode(((Status)record.StatusId).ToString()),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(inventoryUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting inventoryUser data");

                return Json(DataTables.EmptyResult);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetInventoryUserDataById(string inventoryUserId)
        {
            var getInventoryUserByIdQuery = new GetInventoryUserByIdQuery(Guid.Parse(inventoryUserId));
            var inventoryUser = await _mediator.Send(getInventoryUserByIdQuery);

            return Json(inventoryUser);
        }


        private async Task RegisterUserAsync(RegisterModel model, Guid userRoleId)
        {
            model.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = IdentityHelper.CreateUser();

                var preUser = await _userManager.FindByNameAsync(model.FirstName);

                if (preUser == null)
                {
                    preUser = await _userManager.FindByEmailAsync(model.Email);
                }

                if (preUser == null)
                {
                    await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                    await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.DateOfBirth = model.DateOfBirth;

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        var callbackUrl = Url.Action(
                            "ConfirmEmail",
                            "Account",
                            values: new { area = "", userId = userId, code = code, returnUrl = model.ReturnUrl },
                            protocol: Request.Scheme);

                        _emailUtility.SendEmail(model.Email, $"{model.FirstName} {model.LastName}",
                            "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    user = preUser;
                }

                if (userRoleId != Guid.Empty)
                {
                    var userRole = await _roleManager.FindByIdAsync(userRoleId.ToString());
                    if (userRole != null)
                    {
                        var existRole = await _userManager.IsInRoleAsync(user, userRole.Name);

                        if (!existRole)
                        {
                            await _userManager.AddToRoleAsync(user, userRole.Name);
                        }
                    }
                }

                //await _userManager.AddClaimAsync(user, new Claim("registered", "registeredAllowed"));
                // await _userManager.AddClaimAsync(user, new Claim("age", age.ToString()));
            }
        }

        #endregion

    }
}
