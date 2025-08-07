using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries
{
    public class GetBankAccountByIdQuery : IRequest<BankAccount>
    {
        public GetBankAccountByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
