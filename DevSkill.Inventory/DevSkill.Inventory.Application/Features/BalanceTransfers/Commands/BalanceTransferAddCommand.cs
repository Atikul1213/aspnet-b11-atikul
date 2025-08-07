using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Commands
{
    public class BalanceTransferAddCommand : IRequest
    {
        public string FromAccountName { get; set; }
        public string ToAccountName { get; set; }
        public int SendingAccountTypeId { get; set; }
        public int ReceiveAccountTypeId { get; set; }
        public Guid SendingAccountId { get; set; }
        public Guid ReceiveAccountId { get; set; }
        public decimal TransferAmount { get; set; }
        public DateTime TransferDate { get; set; }
        public string Note { get; set; }
    }
}
