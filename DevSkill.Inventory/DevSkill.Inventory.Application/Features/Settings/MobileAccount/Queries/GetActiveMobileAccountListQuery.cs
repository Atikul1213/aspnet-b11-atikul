using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries
{
    public class GetActiveMobileAccountListQuery : IRequest<IList<MobileAccount>>
    {
    }
}
