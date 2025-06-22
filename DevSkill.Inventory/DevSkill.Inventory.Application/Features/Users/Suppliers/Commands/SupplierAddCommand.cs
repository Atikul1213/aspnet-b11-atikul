using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.Suppliers.Commands
{
    public class SupplierAddCommand : IRequest
    {
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Company { get; set; }
        public decimal OpeningBalance { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int StatusId { get; set; }
    }
}
