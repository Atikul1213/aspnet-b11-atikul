using MediatR;

namespace DevSkill.Inventory.Application.Features.Users.InventoryUsers.Commands
{
    public class InventoryUserAddCommand : IRequest
    {
        public Guid EmployeeId { get; set; }
        public Guid UserRoleId { get; set; }
        public string Password { get; set; }
        public int StatusId { get; set; }
        public string EmployeeName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Role { get; set; }
    }
}
