using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface ISupplierService
    {
        Task InsertSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
        Task DeleteSupplierAsync(Supplier supplier);
        Task<Supplier> GetSupplierByIdAsync(Guid id);
        Task<IList<Supplier>> GetAllSuppliersAsync();
    }
}
