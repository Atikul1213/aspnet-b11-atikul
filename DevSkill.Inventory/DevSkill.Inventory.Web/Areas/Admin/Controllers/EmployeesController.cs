using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.Departments.Queries;
using DevSkill.Inventory.Application.Features.Users.Employees.Commands;
using DevSkill.Inventory.Application.Features.Users.Employees.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Employees;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    public class EmployeesController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public EmployeesController(IMapper mapper,
            ILogger<EmployeesController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddEmployee UpdateEmployee RemoveEmployee
        public async Task<IActionResult> Index()
        {
            var model = new EmployeeListModel();

            model.AddEmployeeModel.StatusId = (int)Status.Active;

            var departmentsQuery = new GetDepartmentListQuery();
            var departments = await _mediator.Send(departmentsQuery);

            model.AddEmployeeModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.AddEmployeeModel.Departments = EnumHelper.PrepareSelectListFromEntities(departments, d => d.Id, d => d.Name);

            model.UpdateEmployeeModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.UpdateEmployeeModel.Departments = EnumHelper.PrepareSelectListFromEntities(departments, d => d.Id, d => d.Name);

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(AddEmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeAddCommand>(model);
                    await _mediator.Send(employee);

                    TempData["success'"] = "Employee created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create employee");
                }
            }
            TempData["error"] = "Failed to create employee.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeUpdateCommand>(model);
                    await _mediator.Send(employee);
                    TempData["success'"] = "Employee updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update employee");
                }
            }
            TempData["error"] = "Failed to update employee.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveEmployee(Guid id)
        {
            try
            {
                var employee = new EmployeeDeleteCommand(id);
                await _mediator.Send(employee);

                TempData["success'"] = "Employee deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete employee");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSEmployeeJsonData([FromBody] GetEmployeeListQuery model)
        {
            try
            {
                var result = await _mediator.Send(model);

                var employees = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                //HttpUtility.HtmlEncode(record.Company),
                                HttpUtility.HtmlEncode(record.MobileNumber),
                                HttpUtility.HtmlEncode(record.Address),
                                HttpUtility.HtmlEncode(((Status)record.StatusId).ToString()),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting employee data");

                return Json(DataTables.EmptyResult);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeDataById(string employeeId)
        {
            var getEmployeeByIdQuery = new GetEmployeeByIdQuery(Guid.Parse(employeeId));
            var employee = await _mediator.Send(getEmployeeByIdQuery);

            return Json(employee);
        }

        #endregion
    }
}
