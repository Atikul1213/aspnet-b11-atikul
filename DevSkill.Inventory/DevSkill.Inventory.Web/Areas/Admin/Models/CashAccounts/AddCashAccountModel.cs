using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts
{
    public class AddCashAccountModel
    {
        public AddCashAccountModel()
        {
            Status = new List<SelectListItem>();
        }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public decimal Balance { get; set; }
        public decimal CurrentBalance { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
