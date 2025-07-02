using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.Departments.Commands;
using DevSkill.Inventory.Application.Features.Settings.Departments.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Department;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentsController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public DepartmentsController(IMapper mapper,
            ILogger<DepartmentsController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddDepartment UpdateDepartment RemoveDepartment
        public async Task<IActionResult> Index()
        {
            var getDepartmentListQuery = new GetDepartmentListQuery();
            var departments = await _mediator.Send(getDepartmentListQuery);

            var model = new DepartmentListModel();
            model.AddDepartmentModel.StatusId = (int)Status.Active;
            model.AddDepartmentModel.Status = EnumHelper.PrepareSelectList<Status>();

            model.UpdateDepartmentModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var department in departments)
            {
                var departmentModel = _mapper.Map<DepartmentModel>(department);
                departmentModel.Status = ((Status)department.StatusId).ToString();
                departmentModel.CreateOnUtc = department.CreateOnUtc.ToString("dd-MM-yyyy");

                model.Departments.Add(departmentModel);
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(AddDepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var department = _mapper.Map<DepartmentAddCommand>(model);
                    await _mediator.Send(department);

                    TempData["success"] = "Department created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create department");
                }
            }
            TempData["error"] = "Failed to create department.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var department = _mapper.Map<UpdateDepartmentCommand>(model);
                    await _mediator.Send(department);
                    TempData["success"] = "Department updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update department");
                }
            }
            TempData["error"] = "Failed to update department.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveDepartment(Guid id)
        {
            try
            {
                var department = new DepartmentDeleteCommand(id);
                await _mediator.Send(department);

                TempData["success"] = "Department deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete department");
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
