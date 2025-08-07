using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IProductUnitService
    {
        Task InsertProductUnitAsync(ProductUnit productUnit);
        Task UpdateProductUnitAsync(ProductUnit productUnit);
        Task DeleteProductUnitAsync(ProductUnit productUnit);
        Task<ProductUnit> GetProductUnitByIdAsync(Guid id);
        Task<IList<ProductUnit>> GetAllProductUnitsAsync();
    }
}
