using AutoMapper;
using DevSkill.Inventory.Application.Features.Settings.Categories.Commands;
using DevSkill.Inventory.Application.Features.Settings.Categories.Queries;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Extensions;
using DevSkill.Inventory.Web.Areas.Admin.Models.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public CategoryController(IMapper mapper,
            ILogger<CategoryController> logger,
            IMediator mediator)
        {
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region Index AddCategory UpdateCategory RemoveCategory
        public async Task<IActionResult> Index()
        {
            var getCategoryListQuery = new GetCategoryListQuery();
            var categories = await _mediator.Send(getCategoryListQuery);

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
                    var category = _mapper.Map<CategoryAddCommand>(model);
                    await _mediator.Send(category);

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


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _mapper.Map<UpdateCategoryCommand>(model);
                    await _mediator.Send(category);
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
                var category = new CategoryDeleteCommand(id);
                await _mediator.Send(category);

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
