using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Category
{
    public class AddCategoryModel
    {
        public AddCategoryModel()
        {
            Status = new List<Status>();
        }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public IEnumerable<Status> Status { get; set; }
    }
}
