using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class BankAccountRepository : Repository<BankAccount, Guid>, IBankAccountRepository
    {
        public BankAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
