using Demo.Domain.Entities;
using Demo.Domain.Features.Books.Queries;

namespace Demo.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        List<Book> GetLatestBooks();
        Task<(IList<Book>, int, int)> GetPagedBooksAsync(IGetBooksQuery request);
    }
}
