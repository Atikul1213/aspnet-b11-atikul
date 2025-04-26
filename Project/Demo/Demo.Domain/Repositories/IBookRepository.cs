using Demo.Domain.Entities;

namespace Demo.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        List<Book> GetLatestBooks();
    }
}
