using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Fields

        #endregion

        #region Ctor
        public CategoryController()
        {

        }
        #endregion

        #region List Create Edit Delete
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            return View();
        }

        #endregion
    }
}
