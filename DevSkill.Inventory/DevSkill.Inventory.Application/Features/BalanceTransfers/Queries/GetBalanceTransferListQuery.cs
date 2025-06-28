using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.BalanceTransfers.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.BalanceTransfers.Queries
{
    public class GetBalanceTransferListQuery : DataTables, IRequest<(IList<BalanceTransfer> data, int total, int totalDisplay)>, IGetBalanceTransferQuery
    {
    }
}
