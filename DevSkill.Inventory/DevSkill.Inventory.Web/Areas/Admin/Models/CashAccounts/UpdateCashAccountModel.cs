using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.CashAccounts
{
    public class UpdateCashAccountModel
    {
        public UpdateCashAccountModel()
        {
            Status = new List<SelectListItem>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public decimal Balance { get; set; }
        public decimal CurrentBalance { get; set; }
        public IEnumerable<SelectListItem> Status { get; set; }
    }
}
