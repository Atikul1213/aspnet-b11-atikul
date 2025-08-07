using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.MobileAccount
{
    public class AddMobileAccountModel
    {
        public AddMobileAccountModel()
        {
            Status = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public string Owner { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
