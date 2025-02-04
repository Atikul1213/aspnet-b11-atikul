using Demo.Domain;
using Demo.Domain.Repositories;

namespace Demo.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ApplicationUnitOfWork(ApplicationDbContext dbContext,
            IBookRepository bookRepository,
            IAuthorRepository authorRepository) : base(dbContext)
        {
            bookRepository = bookRepository;
            authorRepository = authorRepository;
        }
        public IBookRepository bookRepository { get; private set; }
        public IAuthorRepository authorRepository { get; private set; }
        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
