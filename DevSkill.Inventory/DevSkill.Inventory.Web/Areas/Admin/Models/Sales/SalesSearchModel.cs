namespace DevSkill.Inventory.Web.Areas.Admin.Models.Sales
{
    public class SalesSearchModel
    {
        public string? CustomerName { get; set; }
        public int StatusId { get; set; }
        public int? TotalFrom { get; set; }
        public int? TotalTo { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
