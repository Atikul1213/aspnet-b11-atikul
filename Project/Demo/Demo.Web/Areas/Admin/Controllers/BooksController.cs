using AutoMapper;
using Demo.Application.Features.Authors.Queries;
using Demo.Application.Features.Books.Commands;
using Demo.Application.Features.Books.Queries;
using Demo.Domain;
using Demo.Domain.Services;
using Demo.Web.Areas.Admin.Models.BookModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace Demo.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BooksController : Controller
    {
        #region Fields
        private readonly IBookService _bookService;
        private readonly IMediator _mediator;
        private readonly ILogger<BooksController> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public BooksController(IBookService bookService,
            IMediator mediator,
            ILogger<BooksController> logger,
            IMapper mapper)
        {
            _bookService = bookService;
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public IActionResult IndexApi()
        {
            var model = new BookAddCommand();

            return View(model);
        }

        public IActionResult Index()
        {
            var model = new BookAddCommand();

            return View(model);
        }

        public async Task<IActionResult> Add()
        {

            var model = new AddBookModel();
            var query = new GetAuthorsQuery();
            var authors = await _mediator.Send(query);
            model.Authors = new SelectList(authors, "Id", "Name");
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


        [HttpPost]
        public async Task<JsonResult> GetBooksJsonDataAsync([FromBody] GetBooksQuery bookQuery)
        {
            try
            {
                var (data, total, totalDisplay) = await _mediator.Send(bookQuery);

                var books = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Title),
                                HttpUtility.HtmlEncode(record.AuthorName),
                                record.Price.ToString("C"),
                                record.PublishDate.ToShortDateString(),
                                record.Id.ToString()

                            }).ToArray()
                };

                return Json(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem in getting books");
                return Json(DataTables.EmptyResult);
            }
        }
        #endregion
    }
}
