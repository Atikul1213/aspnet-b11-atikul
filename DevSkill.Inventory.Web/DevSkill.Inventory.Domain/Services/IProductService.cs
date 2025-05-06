using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IProductService
    {
        Task AddProductAsync(Product product);
    }
}
