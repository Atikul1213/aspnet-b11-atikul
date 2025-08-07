using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Settings.MobileAccounts.Queries
{
    public class GetMobileAccountByIdQuery : IRequest<MobileAccount>
    {
        public GetMobileAccountByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
