using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries
{
    public class GetCashAccountListQuery : IRequest<IList<CashAccount>>
    {
    }
}
