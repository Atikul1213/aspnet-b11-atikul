using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Commands
{
    public class CashAccountDeleteCommand : IRequest
    {
        public CashAccountDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
