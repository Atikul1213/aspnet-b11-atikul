using Demo.Domain.Entities;

namespace Demo.Domain.Repositories
{
    public interface IRepository<T, G> where T : IEntity<G>
    {

    }
}
