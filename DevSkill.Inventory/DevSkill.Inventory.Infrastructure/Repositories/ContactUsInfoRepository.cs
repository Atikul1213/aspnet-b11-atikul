using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ContactUsInfoRepository : Repository<ContactUsInfo, Guid>, IContactUsInfoRepository
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        #endregion

        #region Ctor

        public ContactUsInfoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _applicationDbContext = dbContext;
        }

        #endregion

        #region Methods
        public async Task<ContactUsInfo?> GetContactUsInfoAsync()
        {
            return await _applicationDbContext.ContactUsInfo.FirstOrDefaultAsync();
        }

        public Task UpdateContactUsInfoAsync(ContactUsInfo contactUsInfo)
        {
            _applicationDbContext.ContactUsInfo.Update(contactUsInfo);
            return Task.CompletedTask;
        }
        #endregion
    }
}
