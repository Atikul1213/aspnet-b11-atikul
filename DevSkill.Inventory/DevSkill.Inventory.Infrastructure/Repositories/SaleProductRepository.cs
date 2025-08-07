using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SaleProductRepository : Repository<SaleProduct, Guid>, ISaleProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public SaleProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        public async Task<IList<SaleProduct>> GetSaleProductsBySaleIdAsync(Guid saleId)
        {
            return await GetAllWithFilterAsync(x => x.SalesId == saleId);
        }
    }
}
