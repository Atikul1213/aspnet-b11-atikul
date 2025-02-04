using Demo.Domain.Entities;

namespace Demo.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
        void AddBook(Book book);
        List<Book> GetLatestBooks();
    }
}
