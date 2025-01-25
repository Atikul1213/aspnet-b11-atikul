using Demo.Domain;
using Demo.Domain.Repositories;
using Demo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public IBookRepository bookRepository { get; private set; }
        public IAuthorRepository authorRepository { get; private set; }
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            bookRepository = new BookRepository((ApplicationDbContext)dbContext);
            authorRepository = new AuthorRepository((ApplicationDbContext)dbContext);
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
