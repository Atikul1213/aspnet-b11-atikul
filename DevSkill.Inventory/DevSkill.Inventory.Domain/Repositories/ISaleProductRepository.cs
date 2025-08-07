using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface ISaleProductRepository : IRepository<SaleProduct, Guid>
    {
        Task<IList<SaleProduct>> GetSaleProductsBySaleIdAsync(Guid saleId);
    }
}
