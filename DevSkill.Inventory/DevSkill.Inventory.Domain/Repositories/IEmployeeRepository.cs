using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        Task<(IList<Employee> data, int total, int totalDisplay)> GetPagedEmployeeAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search);
    }
}
