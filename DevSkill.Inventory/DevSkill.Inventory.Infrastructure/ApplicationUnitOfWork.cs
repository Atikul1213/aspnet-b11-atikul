using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        #region Ctor
        public ApplicationUnitOfWork(ApplicationDbContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductUnitRepository productUnitRepository,
            IDepartmentRepository departmentRepository,
            IUserRoleRepository userRoleRepository,
            ISupplierRepository supplierRepository,
            IEmployeeRepository employeeRepository,
            IInventoryUserRepository inventoryUserRepository,
            ICustomerRepository customerRepository,
            ICashAccountRepository cashAccountRepository,
            IBankAccountRepository bankAccountRepository,
            IMobileAccountRepository mobileAccountRepository,
            IBalanceTransferRepository balanceTransferRepository) : base(context)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            ProductUnitRepository = productUnitRepository;
            DepartmentRepository = departmentRepository;
            UserRoleRepository = userRoleRepository;
            SupplierRepository = supplierRepository;
            EmployeeRepository = employeeRepository;
            InventoryUserRepository = inventoryUserRepository;
            CustomerRepository = customerRepository;
            CashAccountRepository = cashAccountRepository;
            BankAccountRepository = bankAccountRepository;
            MobileAccountRepository = mobileAccountRepository;
            BalanceTransferRepository = balanceTransferRepository;
        }

        #endregion

        #region Fields

        public IProductRepository ProductRepository { get; private set; }
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductUnitRepository ProductUnitRepository { get; private set; }
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public IUserRoleRepository UserRoleRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }
        public IInventoryUserRepository InventoryUserRepository { get; set; }
        public ICustomerRepository CustomerRepository { get; set; }
        public ICashAccountRepository CashAccountRepository { get; set; }
        public IBankAccountRepository BankAccountRepository { get; set; }
        public IMobileAccountRepository MobileAccountRepository { get; set; }
        public IBalanceTransferRepository BalanceTransferRepository { get; set; }

        #endregion

        #region Methods
        public async Task<(IList<Product> data, int total, int totalDisplay)> GetProductSPAsync(int pageIndex, int pageSize, string? order, ProductSearchDto search)
        {
            var procedureName = "GetProducts";

            var result = await sqlUtility.QueryWithStoredProcedureAsync<Product>(procedureName,
                new Dictionary<string, object>
                {
                    {"PageIndex", pageIndex },
                    {"PageSize", pageSize },
                    {"OrderBy", order },
                    {"PriceFrom", search.PriceFrom },
                    {"PriceTo", search.PriceTo },
                    {"Name", string.IsNullOrEmpty(search.Name) ? null : search.Name },
                    {"Sku", string.IsNullOrEmpty(search.Sku) ? null : search.Sku }
                },
                new Dictionary<string, Type>
                {
                    {"Total", typeof(int) },
                    {"TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }

        #endregion
    }
}
