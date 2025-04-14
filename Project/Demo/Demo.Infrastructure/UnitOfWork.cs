using Demo.Domain;
using Demo.Domain.Utilities;
using Demo.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        protected ISqlUtility sqlUtility { get; private set; }
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            sqlUtility = new SqlUtility(_dbContext.Database.GetDbConnection());
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
