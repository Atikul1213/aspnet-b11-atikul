using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<(IList<Product> data, int total, int totalDisplay)> GetAllProductsAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
