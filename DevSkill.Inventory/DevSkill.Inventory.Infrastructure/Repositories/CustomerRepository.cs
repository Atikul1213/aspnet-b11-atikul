using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
