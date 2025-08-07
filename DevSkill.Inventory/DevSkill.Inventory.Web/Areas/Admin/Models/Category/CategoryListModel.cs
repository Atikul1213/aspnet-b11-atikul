namespace DevSkill.Inventory.Web.Areas.Admin.Models.Category
{
    public class CategoryListModel
    {
        public CategoryListModel()
        {
            AddCategoryModel = new AddCategoryModel();
            UpdateCategoryModel = new UpdateCategoryModel();
            Categories = new List<CategoryModel>();
        }
        public AddCategoryModel AddCategoryModel { get; set; }
        public UpdateCategoryModel UpdateCategoryModel { get; set; }
        public IList<CategoryModel> Categories { get; set; }
    }
}
