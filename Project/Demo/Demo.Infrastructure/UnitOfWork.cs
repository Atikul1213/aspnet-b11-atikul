using Demo.Domain;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
