using Demo.Domain.Dtos;
using Demo.Domain.Entities;

namespace Demo.Domain.Services
{
    public interface IAuthorService
    {
        void AddAuthor(Author author);
        Author GetAuthorById(Guid id);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        (IList<Author> data, int total, int totalDisplay) GetAuthors(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        Task<(IList<Author> data, int total, int totalDisplay)> GetAuthorsSP(int pageIndex, int pageSize, string? order, AuthorSearchDto search);
    }
}
