using AutoMapper;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Commands;
using DevSkill.Inventory.Application.Features.Users.Suppliers.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Supplier;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SuppliersController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<SuppliersController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public SuppliersController(IMapper mapper,
            ILogger<SuppliersController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddSupplier UpdateSupplier RemoveSupplier
        public async Task<IActionResult> Index()
        {
            var model = new SupplierListModel();

            model.AddSupplierModel.StatusId = (int)Status.Active;
            model.AddSupplierModel.Status = EnumHelper.PrepareSelectList<Status>();
            model.UpdateSupplierModel.Status = EnumHelper.PrepareSelectList<Status>();

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSupplier(AddSupplierModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supplier = _mapper.Map<SupplierAddCommand>(model);
                    await _mediator.Send(supplier);

                    TempData["success'"] = "Supplier created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create supplier");
                }
            }
            TempData["error"] = "Failed to create supplier.";

            return RedirectToAction("Index");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var supplier = _mapper.Map<SupplierUpdateCommand>(model);
                    await _mediator.Send(supplier);
                    TempData["success'"] = "Supplier updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update supplier");
                }
            }
            TempData["error"] = "Failed to update supplier.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveSupplier(Guid id)
        {
            try
            {
                var supplier = new SupplierDeleteCommand(id);
                await _mediator.Send(supplier);

                TempData["success'"] = "Supplier deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete supplier");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSSupplierJsonData([FromBody] GetSupplierListQuery model)
        {
            try
            {
                var result = await _mediator.Send(model);

                var suppliers = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Company),
                                HttpUtility.HtmlEncode(record.MobileNumber),
                                HttpUtility.HtmlEncode(record.Address),
                                HttpUtility.HtmlEncode(((Status)record.StatusId).ToString()),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(suppliers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting supplier data");

                return Json(DataTables.EmptyResult);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierDataById(string supplierId)
        {
            var getSupplierByIdQuery = new GetSupplierByIdQuery(Guid.Parse(supplierId));
            var supplier = await _mediator.Send(getSupplierByIdQuery);

            return Json(supplier);
        }

        #endregion
    }
}
