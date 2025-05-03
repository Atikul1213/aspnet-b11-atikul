using Demo.Domain;
using Demo.Domain.Entities;
using Demo.Domain.Features.Books.Queries;
using MediatR;

namespace Demo.Application.Features.Books.Queries
{
    public class GetBooksQuery : DataTables, IRequest<(IList<Book>, int, int)>, IGetBooksQuery
    {
    }
}
