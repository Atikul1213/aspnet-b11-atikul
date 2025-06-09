using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Category
{
    public class EditCategoryModel
    {
        public EditCategoryModel()
        {
            Status = new List<Status>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public IEnumerable<Status> Status { get; set; }
    }
}
