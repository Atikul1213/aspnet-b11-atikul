using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BalanceTransfers.Queries
{
    public class GetAllBalanceTransferQuery : IRequest<IList<BalanceTransfer>>
    {
    }
}
