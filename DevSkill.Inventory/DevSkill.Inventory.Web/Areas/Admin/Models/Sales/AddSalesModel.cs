using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Sales
{
    public class AddSalesModel
    {
        public AddSalesModel()
        {
            Customers = new List<SelectListItem>();
            SalesTypes = new List<SelectListItem>();
            Products = new List<SelectListItem>();
            AccountTypes = new List<SelectListItem>();
            Accounts = new List<SelectListItem>();
            SaleProducts = new List<AddSalesProductModel>();
        }
        public string InvoiceNo { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int StatusId { get; set; }
        public int SalesTypeId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Vat { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public int AccountTypeId { get; set; }
        public Guid AccountNoId { get; set; }
        public string Note { get; set; }
        public string TermsAndConditions { get; set; }
        public IList<AddSalesProductModel> SaleProducts { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }
        public IEnumerable<SelectListItem> SalesTypes { get; set; }
        public IEnumerable<SelectListItem> Products { get; set; }
        public IEnumerable<SelectListItem> AccountTypes { get; set; }
        public IEnumerable<SelectListItem> Accounts { get; set; }

    }
}
