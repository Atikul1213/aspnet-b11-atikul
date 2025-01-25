using Demo.Domain.Entities;
using Demo.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure.Repositories
{
    public class Repository<T, G> : IRepository<T, G> where T : IEntity<G>
    {
        private readonly DbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity) { }
        public void Update(T entity) { }
        public void Delete(T entity) { }
    }
}
