using Amazon;
using Amazon.SQS;
using AutoMapper;
using DevSkill.Inventory.Application.Features.Customers.Queries;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Sales;
using DevSkill.Inventory.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        #region Fields

        private readonly ISalesService _salesService;
        private readonly IMediator _mediator;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static readonly RegionEndpoint ServiceRegion = RegionEndpoint.USEast1;
        private static IAmazonSQS client;
        private readonly AwsOptions _awsOptions;

        #endregion

        #region Ctor
        public SalesController(ISalesService salesService,
            IMediator mediator,
            ILogger<SalesController> logger,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IOptions<AwsOptions> awsOptions)
        {
            _salesService = salesService;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _awsOptions = awsOptions.Value;
            client = new AmazonSQSClient(ServiceRegion);
        }
        #endregion

        #region Sales Add Edit Delete Index using CQRS

        public async Task<IActionResult> Index()
        {
            var model = new SalesListModel();

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
        public async Task<IActionResult> AddSales(AddSalesModel model, IFormFile? file)
        {
            var fullPath = string.Empty;
            try
            {

                if (ModelState.IsValid)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string salesImagePath = Path.Combine(wwwRootPath, @"images\saless");

                        fullPath = Path.Combine(salesImagePath, fileName);

                        using (var fileStream = new FileStream(Path.Combine(salesImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        model.ImageUrl = Path.Combine(@"/images/saless", fileName);
                    }

                    string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "barcodes");
                    Directory.CreateDirectory(folder);

                    string fileNameBarcode = $"{model.BarCode}_barcode.png";
                    string savePath = Path.Combine(folder, fileNameBarcode);
                    string imagePath = BarcodeHelper.GenerateSalesBarcode(model.Name, model.BarCode, model.WholeSalePrice, savePath);
                    model.BarcodeImagePath = Path.Combine(@"/images/barcodes", fileNameBarcode);

                    var category = await _mediator.Send(new GetCategoryByIdQuery(model.CategoryId));
                    model.CategoryName = category.Name;
                    model.Stock = model.LowStock;
                    var salesAddCommand = _mapper.Map<SalesAddCommand>(model);

                    await _mediator.Send(salesAddCommand);

                    if (model.ImageUrl != null)
                    {
                        await SentMessageInSQS(model, fullPath);
                    }

                    TempData["success"] = "Sales created successfully";
                    return RedirectToAction("IndexSP");
                }
            }
            catch (DuplicateSalesBarCodeException dex)
            {
                _logger.LogError(dex, "Sales bar code already exists");
                TempData["error"] = dex.Message;
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
        public async Task<IActionResult> UpdateSales(UpdateSalesModel model, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string salesImagePath = Path.Combine(wwwRootPath, @"images\saless");

                        using (var fileStream = new FileStream(Path.Combine(salesImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        model.ImageUrl = Path.Combine(@"/images/saless", fileName);
                    }

                    var salesUpdateCommand = _mapper.Map<SalesUpdateCommand>(model);

                    await _mediator.Send(salesUpdateCommand);
                    TempData["success"] = "Sales updated successfully";

                    return RedirectToAction("SalesIndex");
                }
            }
            catch (DuplicateSalesBarCodeException dex)
            {
                _logger.LogError(dex, "Sales bar code already exists");
                TempData["error"] = dex.Message;
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
                                HttpUtility.HtmlEncode(record.ImageUrl),
                                HttpUtility.HtmlEncode(record.BarCode),
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.CategoryName),
                                record.PurchasePrice.ToString("C"),
                                record.MRPPrice.ToString("C"),
                                record.WholeSalePrice.ToString("C"),
                                record.Stock.ToString(),
                                record.LowStock.ToString(),
                                record.DamageStock.ToString(),
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
