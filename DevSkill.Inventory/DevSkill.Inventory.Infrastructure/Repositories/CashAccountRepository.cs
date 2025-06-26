using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CashAccountRepository : Repository<CashAccount, Guid>, ICashAccountRepository
    {
        public CashAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
