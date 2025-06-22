using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, Guid>
    {
    }
}
