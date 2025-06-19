using AutoMapper;
using Demo.Application.Features.Books.Queries;
using Demo.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Demo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("AllowedSites")]
    public class BooksController : ControllerBase
    {
        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger<BooksController> _logger;
        private readonly IMapper _mapper;
        #endregion
        public BooksController(IMediator mediator,
            ILogger<BooksController> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [Authorize(Policy = "ValidLogin")]
        [HttpPost(Name = "GetBooks")]
        public async Task<object> POST([FromBody] GetBooksQuery bookQuery)
        {
            try
            {
                var (data, total, totalDisplay) = await _mediator.Send(bookQuery);

                var result = new
                {
                    recordsTotal = total,
                    recordsFiltered = totalDisplay,
                    data = (from record in data
                            select new string[]
                            {
                                HttpUtility.HtmlEncode(record.Title),
                                HttpUtility.HtmlEncode(record.Author.Name),
                                record.Price.ToString("C"),
                                record.PublishDate.ToShortDateString(),
                                record.Id.ToString()

                            }).ToArray()
                };

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There was a problem in getting books");
                return DataTables.EmptyResult;
            }
        }

    }
}
