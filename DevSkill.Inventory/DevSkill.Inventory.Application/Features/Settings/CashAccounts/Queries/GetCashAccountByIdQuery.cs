using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.CashAccounts.Queries
{
    public class GetCashAccountByIdQuery : IRequest<CashAccount>
    {
        public GetCashAccountByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
