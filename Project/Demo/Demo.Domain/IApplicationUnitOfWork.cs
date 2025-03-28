using Demo.Domain.Repositories;

namespace Demo.Domain
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
    }
}
