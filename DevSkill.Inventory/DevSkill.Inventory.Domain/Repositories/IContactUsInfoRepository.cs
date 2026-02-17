
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Infrastructure.Repositories;

namespace DevSkill.Inventory.Domain.Repositories
{
    public interface IContactUsInfoRepository : IRepository<ContactUsInfo, Guid>
    {
        Task<ContactUsInfo> GetContactUsInfoAsync();
        Task UpdateContactUsInfoAsync(ContactUsInfo contactUsInfo);
    }
}
