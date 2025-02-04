using Demo.Domain.Entities;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        #region Fields
        private readonly IBookService _bookService;
        #endregion

        #region Ctor
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            var model = new AddBookModel();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddBook(AddBookModel model)
        {
            if (ModelState.IsValid)
            {
                _bookService.AddBook(new Book { Title = model.Title });
            }
            return View();
        }
        #endregion
    }
}
