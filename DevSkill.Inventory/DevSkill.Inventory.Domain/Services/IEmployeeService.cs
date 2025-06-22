using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IEmployeeService
    {
        Task InsertEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(Guid id);
        Task<IList<Employee>> GetAllEmployeesAsync();
    }
}
