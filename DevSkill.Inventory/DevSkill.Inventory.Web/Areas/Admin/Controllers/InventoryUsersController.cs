using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.UserRoles.Queries;
using DevSkill.Inventory.Application.Features.Users.Employees.Queries;
using DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands;
using DevSkill.Inventory.Application.Features.Users.InventoryUsers.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.InventoryUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        #endregion

        #region Ctor
        public InventoryUsersController(IMapper mapper,
            ILogger<InventoryUsersController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddInventoryUser UpdateInventoryUser RemoveInventoryUser
        public async Task<IActionResult> Index()
        {
            var model = new InventoryUserListModel();

            var employees = await _mediator.Send(new GetAllEmployeesQuery());
            var employeeSelecttList = EnumHelper.PrepareSelectListFromEntities(employees, d => d.Id, d => d.Name);
            employeeSelecttList.Insert(0, new SelectListItem
            {
                Text = "Select One",
                Value = Guid.Empty.ToString()
            });

            var userRoles = await _mediator.Send(new GetUserRoleListQuery());
            var userRoleSelecttList = EnumHelper.PrepareSelectListFromEntities(userRoles, d => d.Id, d => d.Name);
            userRoleSelecttList.Insert(0, new SelectListItem
            {
                Text = "Select type",
                Value = Guid.Empty.ToString()
            });

            model.AddInventoryUserModel.StatusId = (int)Status.Active;
            model.AddInventoryUserModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.AddInventoryUserModel.Employees = employeeSelecttList;
            model.AddInventoryUserModel.UserRoles = userRoleSelecttList;

            model.UpdateInventoryUserModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.UpdateInventoryUserModel.Employees = employeeSelecttList;
            model.UpdateInventoryUserModel.UserRoles = userRoleSelecttList;

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInventoryUser(AddInventoryUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var inventoryUser = _mapper.Map<InventoryUserAddCommand>(model);
                    var employee = await _mediator.Send(new GetEmployeeByIdQuery(model.EmployeeId));
                    var userRole = await _mediator.Send(new GetUserRoleByIdQuery(model.UserRoleId));

                    inventoryUser.EmployeeName = employee.Name;
                    inventoryUser.Company = ((Company)userRole.CompanyId).ToString();
                    inventoryUser.Email = employee.Email;
                    inventoryUser.MobileNumber = employee.MobileNumber;
                    inventoryUser.Role = userRole.Name;

                    await _mediator.Send(inventoryUser);

                    TempData["success'"] = "InventoryUser created successfully.";

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
        public async Task<IActionResult> UpdateInventoryUser(UpdateInventoryUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var inventoryUser = _mapper.Map<InventoryUserUpdateCommand>(model);
                    var employee = await _mediator.Send(new GetEmployeeByIdQuery(model.EmployeeId));
                    var userRole = await _mediator.Send(new GetUserRoleByIdQuery(model.UserRoleId));

                    inventoryUser.EmployeeName = employee.Name;
                    inventoryUser.Company = ((Company)userRole.CompanyId).ToString();
                    inventoryUser.Email = employee.Email;
                    inventoryUser.MobileNumber = employee.MobileNumber;
                    inventoryUser.Role = userRole.Name;


                    await _mediator.Send(inventoryUser);
                    TempData["success'"] = "InventoryUser updated successfully.";

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


        public async Task<IActionResult> RemoveInventoryUser(Guid id)
        {
            try
            {
                var inventoryUser = new InventoryUserDeleteCommand(id);
                await _mediator.Send(inventoryUser);

                TempData["success'"] = "InventoryUser deleted successfully.";
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

        #endregion
    }
}
