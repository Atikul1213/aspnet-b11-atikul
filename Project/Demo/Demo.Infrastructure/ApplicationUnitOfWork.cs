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
            BookRepository = bookRepository;
            AuthorRepository = authorRepository;
        }
        public IBookRepository BookRepository { get; private set; }
        public IAuthorRepository AuthorRepository { get; private set; }
    }
}
