using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class MobileAccountRepository : Repository<MobileAccount, Guid>, IMobileAccountRepository
    {
        public MobileAccountRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
