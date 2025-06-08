using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
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

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetCQRSPagedProductAsync(IGetProductQuery request)
        {
            if (string.IsNullOrEmpty(request.Search.Value))
                return await GetDynamicAsync(null, request.FormatSortExpression("Id", "Name", "Sku", "Price"), null, request.PageIndex, request.PageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(request.Search.Value) || x.Sku.Contains(request.Search.Value), request.FormatSortExpression("Id", "Name", "Sku", "Price"), null, request.PageIndex, request.PageSize, true);
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
