using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands
{
    public class CashAccountAddCommand : IRequest
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public decimal Balance { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
