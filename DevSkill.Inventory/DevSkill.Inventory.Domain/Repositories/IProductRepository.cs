using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
        Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        Task<(IList<Product> data, int total, int totalDisplay)> GetCQRSPagedProductAsync(IGetProductQuery request);
        Task<bool> CheckBarCodeDuplicateAsync(string barCode, Guid? id = null);
    }
}
