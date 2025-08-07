using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Unit
{
    public class AddUnitModel
    {
        public AddUnitModel()
        {
            Status = new List<SelectListItem>();
        }

        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreateOnUtc { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
