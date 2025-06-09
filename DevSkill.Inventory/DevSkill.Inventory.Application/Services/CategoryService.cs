using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;

namespace DevSkill.Inventory.Application.Services
{
    public class CategoryService : ICategoryService
    {
        #region Fields

        private readonly ICategoryService _categoryService;

        #endregion

        #region Ctor

        public CategoryService(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion
        public Task DeleteCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task InsertCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
