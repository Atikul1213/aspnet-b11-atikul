using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface IDepartmentService
    {
        Task InsertDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task DeleteDepartmentAsync(Department department);
        Task<Department> GetDepartmentByIdAsync(Guid id);
        Task<IList<Department>> GetAllDepartmentsAsync();
    }
}
