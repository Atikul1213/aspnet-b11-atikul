using AutoMapper;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Fields
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<Category> _logger;
        #endregion

        #region Ctor
        public CategoryController(ICategoryService categoryService,
            IMapper mapper,
            ILogger<Category> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        #region Index AddCategory UpdateCategory
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var model = new CategoryListModel();
            model.AddCategoryModel.StatusId = (int)Status.Active;
            model.AddCategoryModel.Status = EnumHelper.PrepareSelectList<Status>();

            model.UpdateCategoryModel.Status = EnumHelper.PrepareSelectList<Status>();

            foreach (var category in categories)
            {
                var categoryModel = _mapper.Map<CategoryModel>(category);
                categoryModel.Status = ((Status)category.StatusId).ToString();
                categoryModel.CreateOnUtc = category.CreateOnUtc.ToString("dd-MM-yyyy");

                model.Categories.Add(categoryModel);
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(AddCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateOnUtc = DateTime.UtcNow;
                    var category = _mapper.Map<Category>(model);
                    await _categoryService.InsertCategoryAsync(category);
                    TempData["success'"] = "Category created successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create category");
                }
            }
            TempData["error"] = "Failed to create category.";

            return RedirectToAction("Index");
        }


        //public async Task<IActionResult> UpdateCategory(Guid id)
        //{
        //    var model = new UpdateCategoryModel();
        //    try
        //    {
        //        var category = await _categoryService.GetCategoryByIdAsync(id);
        //        model = _mapper.Map<UpdateCategoryModel>(category);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Failed to load category for update");
        //        TempData["error"] = "Failed to load category for update.";

        //        return RedirectToAction("Index");
        //    }

        //    model.Status = EnumHelper.PrepareSelectList<Status>();

        //    return View(model);
        //}

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateOnUtc = DateTime.UtcNow;
                    var category = _mapper.Map<Category>(model);
                    await _categoryService.UpdateCategoryAsync(category);
                    TempData["success'"] = "Category updated successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to update category");
                }
            }
            TempData["error"] = "Failed to update category.";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> RemoveCategory(Guid id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                await _categoryService.DeleteCategoryAsync(category);
                TempData["success'"] = "Category deleted successfully.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete category");
            }

            return RedirectToAction("Index");
        }
        #endregion
    }
}
