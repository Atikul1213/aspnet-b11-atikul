using AutoMapper;
using DevSkill.Inventory.Application.Exceptions;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public ProductsController(IProductService productService,
            IMediator mediator,
            ILogger<ProductsController> logger,
            IMapper mapper)
        {
            _productService = productService;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
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
                    product.CreateOnUtc = DateTime.UtcNow;

                    await _productService.AddProductAsync(product);

                    TempData["success"] = "Product created successfully";
                    return RedirectToAction("Index");
                }
                catch (DuplicateProductSkuException dex)
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

                }
                catch (Exception ex)
                {

                }
            }

            return View(model);
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
                                HttpUtility.HtmlEncode(record.Sku),
                                record.Price.ToString("C"),
                                record.Quantity.ToString(),
                                record.IsAvailable ? "True" : "False",
                                record.CreateOnUtc.ToString("dd/MM/yyyy"),
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
