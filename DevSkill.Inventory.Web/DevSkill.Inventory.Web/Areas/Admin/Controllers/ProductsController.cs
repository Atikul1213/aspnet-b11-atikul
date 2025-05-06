using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Web.Areas.Admin.Models.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        #region Fields
        private readonly IProductService _productService;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public ProductsController(IProductService productService,
            IMediator mediator)
        {
            _productService = productService;
            _mediator = mediator;
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
                var product = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    IsAvailable = model.IsAvailable,
                    CreateOnUtc = DateTime.UtcNow
                };

                await _productService.AddProductAsync(product);
            }

            return View(model);
        }


        [HttpPost]
        public IActionResult GetProductJsonData([FromBody] ProductListModel model)
        {

            return Json(true);
        }
        #endregion
    }
}
