using DevSkill.Inventory.Web.Areas.Admin.Models.Customers;

namespace DevSkill.Inventory.Web.Areas.Admin.Models.Sales
{
    public class ShowSalesModel
    {
        public ShowSalesModel()
        {
            Customer = new CustomerModel();
            SaleProducts = new List<SalesProductModel>();
        }
        public CustomerModel Customer { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime SaleDate { get; set; }
        public string CompanyName { get; set; }
        public decimal Vat { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string Note { get; set; }
        public string TermsAndConditions { get; set; }
        public IList<SalesProductModel> SaleProducts { get; set; }
    }
}
