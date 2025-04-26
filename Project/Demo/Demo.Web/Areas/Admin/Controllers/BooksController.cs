using Demo.Application.Features.Books.Commands;
using Demo.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        #region Fields
        private readonly IBookService _bookService;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public BooksController(IBookService bookService,
            IMediator mediator)
        {
            _bookService = bookService;
            _mediator = mediator;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            var model = new BookAddCommand();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(BookAddCommand bookAddCommand)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(bookAddCommand);
            }

            return View(bookAddCommand);
        }
        #endregion
    }
}
