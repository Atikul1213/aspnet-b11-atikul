using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class CustomerRepository : Repository<Customer, Guid>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IList<Customer> data, int total, int totalDisplay)> GetCQRSPagedCustomersAsync(IGetProductQuery request)
        {
            if (string.IsNullOrEmpty(request.Search.Value))
                return await GetDynamicAsync(null, request.FormatSortExpression("Id", "CompanyName"), null, request.PageIndex, request.PageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(request.Search.Value) || x.CompanyName.Contains(request.Search.Value), request.FormatSortExpression("Id", "CompanyName"), null, request.PageIndex, request.PageSize, true);
        }
    }
}
