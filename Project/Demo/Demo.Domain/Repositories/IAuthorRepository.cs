using Demo.Domain.Entities;

namespace Demo.Domain.Repositories
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
    }
}
