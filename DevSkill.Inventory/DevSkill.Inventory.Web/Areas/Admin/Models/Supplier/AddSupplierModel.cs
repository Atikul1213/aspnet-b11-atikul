using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Supplier
{
    public class AddSupplierModel
    {
        public AddSupplierModel()
        {
            Status = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
