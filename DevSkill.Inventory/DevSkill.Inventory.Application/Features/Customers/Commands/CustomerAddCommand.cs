using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Commands
{
    public class CustomerAddCommand : IRequest
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal OpeningBalance { get; set; }
        public string ImageUrl { get; set; }
        public int StatusId { get; set; }
    }
}
