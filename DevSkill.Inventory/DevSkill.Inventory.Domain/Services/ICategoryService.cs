using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface ICategoryService
    {
        Task InsertCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task<IList<Category>> GetAllCategoriesAsync();
    }
}
