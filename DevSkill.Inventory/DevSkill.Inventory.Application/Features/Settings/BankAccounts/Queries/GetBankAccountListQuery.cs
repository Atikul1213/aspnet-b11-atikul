using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries
{
    public class GetBankAccountListQuery : IRequest<IList<BankAccount>>
    {
    }
}
