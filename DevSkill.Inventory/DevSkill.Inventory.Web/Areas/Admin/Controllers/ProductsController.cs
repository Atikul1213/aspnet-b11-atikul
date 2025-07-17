using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using AutoMapper;
using DevSkill.Inventory.Application.Exceptions;
using DevSkill.Inventory.Application.Features.Products.Commands;
using DevSkill.Inventory.Application.Features.Products.Queries;
using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
using DevSkill.Inventory.Application.Features.Settings.Units.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Extensions;
using DevSkill.Inventory.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin, SuperAdmin")]
    //[Authorize(Policy = "AdministratorsPermission")]

    public class ProductsController : Controller
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static readonly RegionEndpoint ServiceRegion = RegionEndpoint.USEast1;
        private static IAmazonSQS client;
        private readonly AwsOptions _awsOptions;

        #endregion

        #region Ctor
        public ProductsController(IProductService productService,
            IMediator mediator,
            ILogger<ProductsController> logger,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment,
            IOptions<AwsOptions> awsOptions)
        {
            _productService = productService;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _awsOptions = awsOptions.Value;
            client = new AmazonSQSClient(ServiceRegion);
        }
        #endregion

        #region Product IndexSP AddProduct UpdateProduct DeleteProduct GetCQRSProductSPJsonData

        public async Task<IActionResult> IndexSP()
        {
            var model = new ProductListModel();

            var categories = await _mediator.Send(new GetActiveCategoryListQuery());
            var units = await _mediator.Send(new GetActiveUnitListQuery());

            model.AddProductModel.Categories = EnumHelper.PrepareSelectListFromEntities(categories, c => c.Id, c => c.Name);
            model.AddProductModel.Units = EnumHelper.PrepareSelectListFromEntities(units, c => c.Id, c => c.Name);

            Random random = new Random();
            int threeDigitNumber = random.Next(100, 1000);
            model.AddProductModel.BarCode = $"P-SUN000{threeDigitNumber}";

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductModel model, IFormFile? file)
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
                        string productImagePath = Path.Combine(wwwRootPath, @"images\products");

                        fullPath = Path.Combine(productImagePath, fileName);

                        using (var fileStream = new FileStream(Path.Combine(productImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        model.ImageUrl = Path.Combine(@"/images/products", fileName);
                    }

                    string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "barcodes");
                    Directory.CreateDirectory(folder);

                    string fileNameBarcode = $"{model.BarCode}_barcode.png";
                    string savePath = Path.Combine(folder, fileNameBarcode);
                    string imagePath = BarcodeHelper.GenerateProductBarcode(model.Name, model.BarCode, model.WholeSalePrice, savePath);
                    model.BarcodeImagePath = Path.Combine(@"/images/barcodes", fileNameBarcode);

                    var category = await _mediator.Send(new GetCategoryByIdQuery(model.CategoryId));
                    model.CategoryName = category.Name;

                    model.Stock = model.LowStock;
                    var productAddCommand = _mapper.Map<ProductAddCommand>(model);

                    await _mediator.Send(productAddCommand);

                    if (model.ImageUrl != null)
                    {
                        //await SentMessageInSQS(model, fullPath);
                    }

                    TempData["success"] = "Product created successfully";
                    return RedirectToAction("IndexSP");
                }
            }
            catch (DuplicateProductBarCodeException dex)
            {
                _logger.LogError(dex, "Product bar code already exists");
                TempData["error"] = dex.Message;
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to create product.";
                _logger.LogError(ex, "There was an error while creating product");
            }

            return RedirectToAction("IndexSP");
        }


        private async Task SentMessageInSQS(AddProductModel model, string fullPath)
        {
            //var createQueueResponse = await CreateQueue(client, QueueName);

            Dictionary<string, MessageAttributeValue> messageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                { "ProductName",   new MessageAttributeValue { DataType = "String", StringValue = model.Name } },
                { "BarCode",  new MessageAttributeValue { DataType = "String", StringValue = model.BarCode } },
                { "ImageUrl",  new MessageAttributeValue { DataType = "String", StringValue = model.ImageUrl } },
                { "ImagePath",  new MessageAttributeValue { DataType = "String", StringValue = fullPath } },
                { "WholeSalePrice", new MessageAttributeValue { DataType = "String", StringValue = model.WholeSalePrice.ToString() } },
            };

            var body = $"Add {model.Name} into the SQS message queue";
            var result = await AWSManager.SendMessage(client, _awsOptions.SQSUrl, body, messageAttributes);
        }

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            try
            {
                var product = await _mediator.Send(new GetProductByIdQuery(id));

                if (product is null)
                    return RedirectToAction("IndexSP");

                var model = _mapper.Map<UpdateProductModel>(product);
                var categories = await _mediator.Send(new GetActiveCategoryListQuery());
                var units = await _mediator.Send(new GetActiveUnitListQuery());

                model.Categories = EnumHelper.PrepareSelectListFromEntities(categories, c => c.Id, c => c.Name);

                model.Units = EnumHelper.PrepareSelectListFromEntities(units, c => c.Id, c => c.Name);

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["error"] = "No product found with the Id";
                _logger.LogError(ex, "No product found with the Id");
            }

            return RedirectToAction("IndexSP");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProduct(UpdateProductModel model, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string productImagePath = Path.Combine(wwwRootPath, @"images\products");

                        using (var fileStream = new FileStream(Path.Combine(productImagePath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        model.ImageUrl = Path.Combine(@"/images/products", fileName);
                    }

                    var category = await _mediator.Send(new GetCategoryByIdQuery(model.CategoryId));
                    model.CategoryName = category.Name;

                    var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(model);
                    await _mediator.Send(productUpdateCommand);
                    TempData["success"] = "Product updated successfully";

                    return RedirectToAction("IndexSP");
                }
            }
            catch (DuplicateProductBarCodeException dex)
            {
                _logger.LogError(dex, "Product bar code already exists");
                TempData["error"] = dex.Message;
            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to edit product.";
                _logger.LogError(ex, "There was an error while updating product");
            }
            var categories = await _mediator.Send(new GetCategoryListQuery());
            var units = await _mediator.Send(new GetUnitListQuery());


            model.Categories = EnumHelper.PrepareSelectListFromEntities(categories, c => c.Id, c => c.Name);
            model.Units = EnumHelper.PrepareSelectListFromEntities(units, c => c.Id, c => c.Name);

            return View(model);
        }

        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var productDeleteCommand = new ProductDeleteCommand(id);
                await _mediator.Send(productDeleteCommand);
                ViewData["success"] = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Failed to delete product";
                _logger.LogError(ex, "There was an error while deleting product");
            }

            return RedirectToAction("IndexSP");
        }


        [HttpPost]
        public async Task<IActionResult> GetCQRSProductSPJsonData([FromBody] GetAllProductQuery model)
        {
            try
            {
                var (data, total, totalDisplay) = await _mediator.Send(model);
                var products = new
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

                return Json(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting product data");
                return Json(DataTables.EmptyResult);
            }
        }

        #endregion

        #region Index Create Edit Delete GetProductJsonData   without CQRS
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexSP2()
        {
            var model = new ProductListModel();

            return View(model);
        }

        public IActionResult Create()
        {
            var model = new AddProductModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProductModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(model);

                    await _productService.AddProductAsync(product);

                    TempData["success"] = "Product created successfully";
                    return RedirectToAction("Index");
                }
                catch (DuplicateProductBarCodeException dex)
                {
                    _logger.LogError(dex, "Product SKU already exists");
                    TempData["error"] = dex.Message;
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Failed to create product";
                    _logger.LogError(ex, "There was an error while creating product");
                }
            }

            return View(model);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                TempData["error"] = "Product deos not with the specified id";

                return RedirectToAction("Index");
            }

            var model = _mapper.Map<UpdateProductModel>(product);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateProductModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var product = _mapper.Map<Product>(model);

                    await _productService.UpdateProductAsync(product);
                    TempData["success"] = "Product updated successfully";

                    return RedirectToAction("Index");
                }
                catch (DuplicateProductBarCodeException dex)
                {
                    _logger.LogError(dex, "Product SKU already exists");
                    TempData["error"] = dex.Message;
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Failed to update product";
                    _logger.LogError(ex, "There was an error while updating product");
                }
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        // [Authorize(Policy = "CustomAccess")]
        // [Authorize(Policy = "AgeRestriction")]  
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _productService.DeleteProductAsync(id);
                ViewData["success"] = "Product deleted successfully";
            }
            catch (Exception ex)
            {
                ViewData["error"] = "Failed to delete product";
                _logger.LogError(ex, "There was an error while deleting product");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetProductJsonData([FromBody] ProductListModel model)
        {
            try
            {
                var result = await _productService.GetAllProductsAsync(model.PageIndex, model.PageSize, model.FormatSortExpression("Name", "Id"), model.Search);

                var products = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                //HttpUtility.HtmlEncode(record.Sku),
                                //record.Price.ToString("C"),
                                //record.Quantity.ToString(),
                                //record.IsAvailable ? "True" : "False",
                                //record.CreateOnUtc.ToString("dd/MM/yyyy"),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting product data");

                return Json(DataTables.EmptyResult);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetProductSPJsonData([FromBody] ProductListModel model)
        {
            try
            {
                var productSearchDto = _mapper.Map<ProductSearchDto>(model.SearchItem);

                var result = await _productService.GetAllSPProductsAsync(model.PageIndex, model.PageSize, model.FormatSortExpression("Name", "Sku", "Price", "Id"), productSearchDto);

                var products = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                //HttpUtility.HtmlEncode(record.Sku),
                                //record.Price.ToString("C"),
                                //record.Quantity.ToString(),
                                //record.IsAvailable ? "True" : "False",
                                //record.CreateOnUtc.ToString("dd/MM/yyyy"),
                                record.Id.ToString()
                            }).ToArray()
                };

                return Json(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error while getting product data");

                return Json(DataTables.EmptyResult);
            }
        }

        #endregion
    }
}
