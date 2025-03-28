using Demo.Domain.Entities;

namespace Demo.Domain.Features.Books.Queries
{
    public interface IBookGetQuery
    {
        Book Get(Guid id);
    }
}
