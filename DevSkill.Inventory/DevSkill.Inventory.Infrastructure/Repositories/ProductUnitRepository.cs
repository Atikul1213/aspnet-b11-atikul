using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductUnitRepository : Repository<ProductUnit, Guid>, IProductUnitRepository
    {
        public ProductUnitRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
