using Demo.Domain.Entities;
using Demo.Domain.Repositories;

namespace Demo.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book, Guid>, IBookRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BookRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBook(Book book)
        {
            // _dbContext.Books.Add(book);
        }

        public List<Book> GetLatestBooks()
        {
            var date = DateTime.Now.AddDays(-10);
            var books = _dbContext.Books.Where(x => x.PublishDate > date).ToList();
            return books;
        }

    }
}
