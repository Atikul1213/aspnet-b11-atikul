using Demo.Domain.Dtos;
using Demo.Domain.Entities;
using Demo.Domain.Repositories;

namespace Demo.Domain
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IBookRepository BookRepository { get; }
        public IAuthorRepository AuthorRepository { get; }
        Task<(IList<Author> data, int total, int totalDisplay)> GetAuthorsSP(int pageIndex, int pageSize, string? order, AuthorSearchDto search);
    }
}
