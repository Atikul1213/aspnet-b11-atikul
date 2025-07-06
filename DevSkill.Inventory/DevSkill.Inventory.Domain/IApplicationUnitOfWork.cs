using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Domain
{
    public interface IApplicationUnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductUnitRepository ProductUnitRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IUserRoleRepository UserRoleRepository { get; }
        public ISupplierRepository SupplierRepository { get; }
        public IEmployeeRepository EmployeeRepository { get; }
        public IInventoryUserRepository InventoryUserRepository { get; }
        public ICustomerRepository CustomerRepository { get; }
        public ICashAccountRepository CashAccountRepository { get; }
        public IBankAccountRepository BankAccountRepository { get; }
        public IMobileAccountRepository MobileAccountRepository { get; }
        public IBalanceTransferRepository BalanceTransferRepository { get; }
        public ISalesRepository SalesRepository { get; set; }
        Task<(IList<Product> data, int total, int totalDisplay)> GetProductSPAsync(int pageIndex, int pageSize, string? order, ProductSearchDto search);
    }
}
