using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Utilities;
using DevSkill.Inventory.Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly DbContext _dbContext;
        protected ISqlUtility sqlUtility { get; private set; }
        #endregion

        #region Ctor
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            sqlUtility = new SqlUtility(_dbContext.Database.GetDbConnection());
        }
        #endregion

        #region Methods
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
