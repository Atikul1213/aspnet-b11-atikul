using Demo.Domain;
using Demo.Domain.Utilities;
using Demo.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastructure
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        public ISqlUtility SqlUtility { get; private set; }

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            SqlUtility = new SqlUtility(_dbContext.Database.GetDbConnection());
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
