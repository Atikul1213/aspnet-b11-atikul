using Demo.Domain.Entities;

namespace Demo.Domain.Services
{
    public interface IAuthorService
    {
        void AddAuthor(Author author);
        Author GetAuthorById(Guid id);
        void DeleteAuthor(Author author);
        (IList<Author> data, int total, int totalDisplay) GetAuthors(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
