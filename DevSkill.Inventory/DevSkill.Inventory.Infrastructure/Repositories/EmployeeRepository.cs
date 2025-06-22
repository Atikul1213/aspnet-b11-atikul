using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<(IList<Employee> data, int total, int totalDisplay)> GetPagedEmployeeAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            if (string.IsNullOrEmpty(search.Value))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }
    }
}
