using Demo.Domain;
using Demo.Domain.Dtos;
using Demo.Domain.Features.Books.Queries;
using MediatR;

namespace Demo.Application.Features.Books.Queries
{
    public class GetBooksQuery : DataTables, IRequest<(IList<BookWithAuthorDto>, int, int)>, IGetBooksQuery
    {
        public BookSearchDto SearchItem { get; set; }
    }
}
