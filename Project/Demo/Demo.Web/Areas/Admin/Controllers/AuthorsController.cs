using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        #region Fields
        private readonly IAuthorService _authorService;
        private readonly ILogger<AuthorsController> _logger;
        #endregion

        #region Ctor
        public AuthorsController(IAuthorService authorService,
            ILogger<AuthorsController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        #endregion

        #region Index / AddAuthor / GetAuthorJsonData
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
                var author = new Author()
                {
                    Name = model.Name,
                    Biography = model.Biography,
                    Rating = model.Rating
                };

                _authorService.AddAuthor(author);
            }

            return View(model);
        }

        public JsonResult GetAuthorJsonData(AuthorListModel model)
        {
            try
            {
                var result = _authorService.GetAuthors(model.PageIndex, model.PageSize, model.FormatSortExpression("Name", "Biography", "Rating", "Id"), model.Search);

                var authors = new
                {
                    recordsTotal = result.total,
                    recordsFiltered = result.totalDisplay,
                    data = (from record in result.data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Name),
                                HttpUtility.HtmlEncode(record.Biography),
                                record.Rating.ToString(),
                                record.Id.ToString(),
                            }).ToArray()
                };

                return Json(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was an error getting the authors list.");
                return Json(DataTables.EmptyResult);
            }

        }

        #endregion
    }
}
