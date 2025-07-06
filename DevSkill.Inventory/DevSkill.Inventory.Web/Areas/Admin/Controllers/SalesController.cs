using AutoMapper;
using DevSkill.Inventory.Application.Features.Customers.Queries;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.SalesProduct.Commands;
using DevSkill.Inventory.Application.Features.SalesProduct.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Sales;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        #region Fields

        private readonly IMediator _mediator;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Ctor
        public SalesController(IMediator mediator,
            ILogger<SalesController> logger,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Sales Add Edit Delete Index using CQRS

        public async Task<IActionResult> SalesIndex()
        {
            var model = new SalesListModel();

            model.Status = EnumHelper.PrepareSelectList<SalesStatus>();
            model.Status.Insert(0, new SelectListItem
            {
                Text = "Select Status",
                Value = "-1"
            });

            return View(model);
        }

        public async Task<IActionResult> AddSales()
        {
            var model = new AddSalesModel();

            var customers = await _mediator.Send(new GetActiveCustomerListQuery());
            var products = await _mediator.Send(new GetProductListQuery());

            var customerSelectList = EnumHelper.PrepareSelectListFromEntities(customers, c => c.Id, c => c.Name);
            customerSelectList.Insert(0, new SelectListItem
            {
                Text = "Select customer",
                Value = Guid.Empty.ToString()
            });

            model.Customers = customerSelectList;

            var productSelectList = EnumHelper.PrepareSelectListFromEntities(products, c => c.Id, c => c.Name);
            productSelectList.Insert(0, new SelectListItem
            {
                Text = "Select product",
                Value = Guid.Empty.ToString()
            });

            model.Products = productSelectList;

            model.SalesTypes = EnumHelper.PrepareSelectList<SalesType>();
            model.AccountTypes = EnumHelper.PrepareSelectList<AccountType>();

            Random random = new Random();
            int threeDigitNumber = random.Next(100, 1000);
            model.InvoiceNo = $"INV-SUN000{threeDigitNumber}";

            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSales(AddSalesModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //var category = await _mediator.Send(new GetCategoryByIdQuery(model.CategoryId));
                    //model.CategoryName = category.Name;
                    //model.Stock = model.LowStock;
                    //var salesAddCommand = _mapper.Map<SalesAddCommand>(model);

                    //await _mediator.Send(salesAddCommand);

                    //TempData["success"] = "Sales created successfully";
                    return RedirectToAction("IndexSP");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to create sales.";
                _logger.LogError(ex, "There was an error while creating sales");
            }

            return RedirectToAction("IndexSP");
        }

        public async Task<IActionResult> UpdateSales(Guid id)
        {
            try
            {
                var sales = await _mediator.Send(new GetSalesByIdQuery(id));

                if (sales is null)
                    return RedirectToAction("SalesIndex");

                var model = _mapper.Map<UpdateSalesModel>(sales);
                var customers = await _mediator.Send(new GetActiveCustomerListQuery());
                var products = await _mediator.Send(new GetProductListQuery());

                var customerSelectList = EnumHelper.PrepareSelectListFromEntities(customers, c => c.Id, c => c.Name);
                customerSelectList.Insert(0, new SelectListItem
                {
                    Text = "Select customer",
                    Value = Guid.Empty.ToString()
                });

                model.Customers = customerSelectList;

                var productSelectList = EnumHelper.PrepareSelectListFromEntities(products, c => c.Id, c => c.Name);
                productSelectList.Insert(0, new SelectListItem
                {
                    Text = "Select product",
                    Value = Guid.Empty.ToString()
                });

                model.Products = productSelectList;

                model.SalesTypes = EnumHelper.PrepareSelectList<SalesType>();
                model.AccountTypes = EnumHelper.PrepareSelectList<AccountType>();


                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = "No sales found with the Id";
                _logger.LogError(ex, "No sales found with the Id");
            }

            return RedirectToAction("SalesIndex");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSales(UpdateSalesModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var salesUpdateCommand = _mapper.Map<SalesUpdateCommand>(model);

                    await _mediator.Send(salesUpdateCommand);
                    TempData["success"] = "Sales updated successfully";

                    return RedirectToAction("SalesIndex");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to edit sales.";
                _logger.LogError(ex, "There was an error while updating sales");
            }
            var customers = await _mediator.Send(new GetActiveCustomerListQuery());
            var products = await _mediator.Send(new GetProductListQuery());

            var customerSelectList = EnumHelper.PrepareSelectListFromEntities(customers, c => c.Id, c => c.Name);
            customerSelectList.Insert(0, new SelectListItem
            {
                Text = "Select customer",
                Value = Guid.Empty.ToString()
            });

            model.Customers = customerSelectList;

            var productSelectList = EnumHelper.PrepareSelectListFromEntities(products, c => c.Id, c => c.Name);
            productSelectList.Insert(0, new SelectListItem
            {
                Text = "Select product",
                Value = Guid.Empty.ToString()
            });

            model.Products = productSelectList;

            model.SalesTypes = EnumHelper.PrepareSelectList<SalesType>();
            model.AccountTypes = EnumHelper.PrepareSelectList<AccountType>();


            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSales(Guid id)
        {
            try
            {
                var salesDeleteCommand = new SalesDeleteCommand(id);

                await _mediator.Send(salesDeleteCommand);
                ViewData["success"] = "Sales deleted successfully";
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Failed to delete sales";
                _logger.LogError(ex, "There was an error while deleting sales");
            }

            return RedirectToAction("SalesIndex");
        }

        [HttpPost]
        public async Task<IActionResult> GetCQRSSalesSPJsonData([FromBody] GetAllSalesQuery model)
        {
            try
            {
                var (data, total, totalDisplay) = await _mediator.Send(model);

                var saless = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                //HttpUtility.HtmlEncode(record.ImageUrl),
                                //HttpUtility.HtmlEncode(record.BarCode),
                                //HttpUtility.HtmlEncode(record.Name),
                                //HttpUtility.HtmlEncode(record.CategoryName),
                                //record.PurchasePrice.ToString("C"),
                                //record.MRPPrice.ToString("C"),
                                //record.WholeSalePrice.ToString("C"),
                                //record.Stock.ToString(),
                                //record.LowStock.ToString(),
                                //record.DamageStock.ToString(),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(saless);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting sales data");

                return Json(DataTables.EmptyResult);
            }
        }



        #endregion
    }
}
