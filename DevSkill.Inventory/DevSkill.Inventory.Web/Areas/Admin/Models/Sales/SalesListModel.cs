using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Sales
{
    public class SalesListModel
    {
        public SalesListModel()
        {
            SearchItem = new SalesSearchModel();
            Status = new List<SelectListItem>();
        }
        public SalesSearchModel SearchItem { get; set; }
        public IList<SelectListItem> Status { get; set; }
    }
}
