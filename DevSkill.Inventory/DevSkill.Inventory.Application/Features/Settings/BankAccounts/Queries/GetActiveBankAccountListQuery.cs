using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.BankAccounts.Queries
{
    public class GetActiveBankAccountListQuery : IRequest<IList<BankAccount>>
    {
    }
}
