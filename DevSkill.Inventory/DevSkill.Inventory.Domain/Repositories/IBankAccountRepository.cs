using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface IBankAccountRepository : IRepository<BankAccount, Guid>
    {
    }
}
