using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface ISaleSaleProductService
    {
        Task AddSaleProductAsync(SaleProduct saleProduct);
        Task UpdateSaleProductAsync(SaleProduct saleProduct);
        Task DeleteSaleProductAsync(Guid id);
        Task<SaleProduct> GetSaleProductByIdAsync(Guid id);
    }
}
