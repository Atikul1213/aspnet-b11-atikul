using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            if (string.IsNullOrEmpty(search.Value))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }

        public async Task<bool> CheckSkuDuplicateAsync(string sku, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id && x.Sku == sku) > 0;
            }

            return await GetCountAsync(x => x.Sku == sku) > 0;
        }
    }
}
