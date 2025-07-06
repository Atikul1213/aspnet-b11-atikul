using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface ISalesService
    {
        Task AddSalesAsync(Sales sales);
        Task UpdateSalesAsync(Sales sales);
        Task DeleteSalesAsync(Guid id);
        Task<Sales> GetSalesByIdAsync(Guid id);
        Task<(IList<Sales> data, int total, int totalDisplay)> GetAllSalessAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
        Task<(IList<Sales> data, int total, int totalDisplay)> GetAllSPSalessAsync(int pageIndex, int pageSize, string? order, SalesSearchDto search);
    }
}
