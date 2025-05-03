using Demo.Domain.Entities;
using Demo.Domain.Features.Books.Queries;
using Demo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
            _dbContext.Books.Add(book);
        }

        public List<Book> GetLatestBooks()
        {
            var date = DateTime.Now.AddDays(-10);
            var books = _dbContext.Books.Where(x => x.PublishDate > date).ToList();
            return books;
        }

        public async Task<(IList<Book>, int, int)> GetPagedBooksAsync(IGetBooksQuery request)
        {
            return await GetDynamicAsync(
                x => x.Title.Contains(request.Search.Value) || x.Author.Name.Contains(request.Search.Value),
                request.FormatSortExpression("Title", "AuthorName", "Price", "PublishDate"),
                y => y.Include(z => z.Author), request.PageIndex, request.PageSize, true);
        }
    }
}
