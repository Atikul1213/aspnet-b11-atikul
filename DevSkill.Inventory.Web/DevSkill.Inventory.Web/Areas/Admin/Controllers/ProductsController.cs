using DevSkill.Inventory.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        #region Fields

        #endregion

        #region Ctor
        public ProductsController()
        {

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

        [HttpPost]
        public IActionResult Create(AddProductModel mode)
        {


            return View(mode);
        }
        #endregion
    }
}
