using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task<(IList<Product> data, int total, int totalDisplay)> GetAllProductsAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
