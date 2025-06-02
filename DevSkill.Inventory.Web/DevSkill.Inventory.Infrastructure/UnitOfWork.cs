using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
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
