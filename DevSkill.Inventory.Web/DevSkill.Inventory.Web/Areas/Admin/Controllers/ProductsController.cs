using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        #region Fields
        private readonly IProductService _productService;
        #endregion

        #region Ctor
        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
        public IActionResult Create(AddProductModel mode)
        {
            if (ModelState.IsValid)
            {

            }

            return View(mode);
        }
        #endregion
    }
}
