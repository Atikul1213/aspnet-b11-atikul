using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Commands
{
    public class SalesUpdateCommand : IRequest<Sales>
    {
        public Guid Id { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhoneNumber { get; set; }
        public int StatusId { get; set; }
        public int SalesTypeId { get; set; }
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
    }
}
