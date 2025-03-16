using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddAuthor()
        {
            var model = new AddAuthorModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddAuthor(AddAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                var author = new Author() { Name = model.Name };

                _authorService.AddAuthor(author);
            }

            return View(model);
        }

        public JsonResult GetAuthorJsonData(AuthorListModel model)
        {
            try
            {
                var result = _authorService.GetAuthors(model.PageIndex, model.PageSize, FormatSortExpression("Name"), model.Search);

                return result;
            }
            catch (Exception ex)
            {

                return EmptyResult;
            }


            return Json(model);
        }
    }
}
