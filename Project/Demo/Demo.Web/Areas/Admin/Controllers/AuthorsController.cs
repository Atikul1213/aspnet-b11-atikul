using AutoMapper;
using Demo.Application.Exceptions;
using Demo.Domain;
using Demo.Domain.Dtos;
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
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public AuthorsController(IAuthorService authorService,
            ILogger<AuthorsController> logger,
            IMapper mapper)
        {
            _authorService = authorService;
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Index / AddAuthor / Edit / Delete / GetAuthorJsonData
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult IndexSP()
        {
            var model = new AuthorListModel();
            return View(model);
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
                try
                {
                    var author = _mapper.Map<Author>(model);
                    author.Id = IdentityGenerator.NewSequentialGuid();
                    _authorService.AddAuthor(author);

                    TempData["success"] = "Author created successfully.";

                    return RedirectToAction("Index");
                }
                catch (DuplicateAuthorNameException dex)
                {
                    TempData["error"] = dex.Message;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add exception in Author Add");
                    TempData["error"] = "Failed to author create.";
                }
            }
            else
                TempData["error"] = "Failed to author create.";

            return View(model);
        }


        public IActionResult Edit(Guid id)
        {
            var author = _authorService.GetAuthorById(id);

            var model = _mapper.Map<UpdateAuthorModel>(author);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateAuthorModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var author = _mapper.Map<Author>(model);
                    _authorService.UpdateAuthor(author);

                    TempData["success"] = "Author Updated Successfully.";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update author");
                    TempData["error"] = "Author update failed.";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var author = _authorService.GetAuthorById(id);

                if (author is not null)
                {
                    _authorService.DeleteAuthor(author);
                    TempData["success"] = "Author Deleted Successfully.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete author");
                TempData["error"] = "Author delete Failed";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetAuthorJsonData([FromBody] AuthorListModel model)
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

        [HttpPost]
        public async Task<JsonResult> GetAuthorJsonDataSP([FromBody] AuthorListModel model)
        {
            try
            {
                var authorSearchDto = _mapper.Map<AuthorSearchDto>(model.SearchItem);
                var result = await _authorService.GetAuthorsSP(model.PageIndex, model.PageSize, model.FormatSortExpression("Name", "Biography", "Rating", "Id"), authorSearchDto);

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
