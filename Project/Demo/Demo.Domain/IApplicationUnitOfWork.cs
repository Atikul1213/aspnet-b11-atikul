using Demo.Domain.Repositories;

namespace Demo.Domain
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IBookRepository bookRepository { get; }
        public IAuthorRepository authorRepository { get; }
    }
}
