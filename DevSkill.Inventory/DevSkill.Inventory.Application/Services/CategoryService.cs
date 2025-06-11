using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;

namespace DevSkill.Inventory.Application.Services
{
    public class CategoryService : ICategoryService
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;

        #endregion

        #region Ctor

        public CategoryService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        #endregion

        #region Methods
        public async Task DeleteCategoryAsync(Category category)
        {
            await _applicationUnitOfWork.CategoryRepository.RemoveAsync(category);
            await _applicationUnitOfWork.SaveAsync();
        }

        public async Task<IList<Category>> GetAllCategoriesAsync()
        {
            var categories = await _applicationUnitOfWork.CategoryRepository.GetAllAsync();
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _applicationUnitOfWork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task InsertCategoryAsync(Category category)
        {
            await _applicationUnitOfWork.CategoryRepository.AddAsync(category);
            await _applicationUnitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _applicationUnitOfWork.CategoryRepository.UpdateAsync(category);
            await _applicationUnitOfWork.SaveAsync();
        }

        #endregion
    }
}
