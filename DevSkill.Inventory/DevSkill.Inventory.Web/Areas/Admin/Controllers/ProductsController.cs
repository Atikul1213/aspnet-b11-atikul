using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using AutoMapper;
using Cortex.Mediator;
using DevSkill.Core.Application;
using DevSkill.Inventory.Application.Exceptions;
using DevSkill.Inventory.Application.Features.Products.Commands.CreateProduct;
using DevSkill.Inventory.Application.Features.Products.Commands.DeleteProduct;
using DevSkill.Inventory.Application.Features.Products.Commands.UpdateProduct;
using DevSkill.Inventory.Application.Features.Products.Queries.GetProductById;
using DevSkill.Inventory.Application.Features.Products.Queries.GetProductList;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using DevSkill.Inventory.Web.Extensions;
using DevSkill.Inventory.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        #region  IndexSP AddProduct UpdateProduct DeleteProduct GetCQRSProductSPJsonData with CQRS

        public async Task<IActionResult> IndexSP()
        {
            var model = new ProductListModel();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            var model = new AddProductModel();
            model.Categories = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select category",
                    Value = Guid.NewGuid().ToString()
                }
            };

            model.Units = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select unit",
                    Value = Guid.NewGuid().ToString()
                }
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(AddProductModel model, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"images\products");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadFolderPath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        model.ImageUrl = Path.Combine(@"/images/products", fileName);
                    }

                    if (model.BarCode != null)
                    {
                        var barCodeFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"images\barcodes");
                        if (!Directory.Exists(barCodeFolderPath))
                        {
                            Directory.CreateDirectory(barCodeFolderPath);
                        }

                        string barCodeFileName = $"{model.BarCode}_barcode.png";
                        string barCodeFilePath = Path.Combine(barCodeFolderPath, barCodeFileName);
                        string imagePath = BarcodeHelper.GenerateProductBarcode(model.Name, model.BarCode, model.WholeSalePrice, barCodeFilePath);

                        model.BarcodeImagePath = Path.Combine(@"/images/barcodes", barCodeFileName);
                    }
                    //var category = await _mediator.Send(new GetCategoryByIdQuery(model.CategoryId));
                    model.CategoryName = "Electronics";

                    model.Stock = model.LowStock;
                    var createProductCommand = _mapper.Map<CreateProductCommand>(model);

                    var result = await _mediator.SendCommandAsync<CreateProductCommand, ResultResponse>(createProductCommand);

                    if (result.IsSuccess)
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

        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            try
            {
                var getProductByIdQuery = new GetProductByIdQuery(id);
                var query = await _mediator.SendQueryAsync<GetProductByIdQuery, ResultResponse<Product>>(getProductByIdQuery);

                if (!query.IsSuccess)
                {
                    TempData["error"] = "Product does not exist";
                    return RedirectToAction("IndexSP");
                }

                var model = _mapper.Map<UpdateProductModel>(query.Data);

                model.Categories = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Select category",
                        Value = Guid.NewGuid().ToString()
                    }
                };

                model.Units = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Select unit",
                        Value = Guid.NewGuid().ToString()
                    }
                };

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
                    if (file != null)
                    {
                        string uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"images\products");
                        if (!Directory.Exists(uploadFolderPath))
                        {
                            Directory.CreateDirectory(uploadFolderPath);
                        }

                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(uploadFolderPath, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        model.ImageUrl = Path.Combine(@"/images/products", fileName);
                    }

                    var updateProductCommand = _mapper.Map<UpdateProductCommand>(model);

                    var result = await _mediator.SendCommandAsync<UpdateProductCommand, ResultResponse>(updateProductCommand);

                    if (result.IsSuccess)
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

            model.Categories = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select category",
                    Value = Guid.NewGuid().ToString()
                }
            };

            model.Units = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text = "Select unit",
                    Value = Guid.NewGuid().ToString()
                }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var deleteProductCommand = new DeleteProductCommand(id);
                var result = await _mediator.SendCommandAsync<DeleteProductCommand, ResultResponse>(deleteProductCommand);

                if (result.IsSuccess)
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
        public async Task<IActionResult> GetCQRSProductSPJsonData([FromBody] ProductListModel model)
        {
            try
            {
                var getProductListQuery = _mapper.Map<GetProductListQuery>(model);

                var (data, total, totalDisplay) = await _mediator.SendQueryAsync<GetProductListQuery, (IList<Product>, int, int)>(getProductListQuery);
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
                int index = 0;
                var products = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                (++index).ToString(),
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

        #region Utilities

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

        #endregion

    }
}
