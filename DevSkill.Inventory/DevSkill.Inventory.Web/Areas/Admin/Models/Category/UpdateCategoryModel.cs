using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Category
{
    public class UpdateCategoryModel
    {
        public UpdateCategoryModel()
        {
            Status = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
