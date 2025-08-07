using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands
{
    public class BalanceTransferDeleteCommand : IRequest
    {
        public BalanceTransferDeleteCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
