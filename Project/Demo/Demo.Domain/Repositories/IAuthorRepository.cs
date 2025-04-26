using Demo.Domain.Entities;

namespace Demo.Domain.Repositories
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        (IList<Author> data, int total, int totalDisplay) GetPagedAuthors(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        bool IsNameDuplicate(string name, Guid? id = null);
    }
}
