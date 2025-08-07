using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries
{
    public class GetMobileAccountListQuery : IRequest<IList<MobileAccount>>
    {
    }
}
