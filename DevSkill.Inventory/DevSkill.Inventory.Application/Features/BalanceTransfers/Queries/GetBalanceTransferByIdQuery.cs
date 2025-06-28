using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Queries
{
    public class GetBalanceTransferByIdQuery : IRequest<BalanceTransfer>
    {
        public GetBalanceTransferByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
