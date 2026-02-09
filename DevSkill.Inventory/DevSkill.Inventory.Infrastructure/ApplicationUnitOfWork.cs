using DevSkill.Core.Domain.EmailRepositoryContracts;
using DevSkill.Core.Infrastructure;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using DevSkill.Inventory.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevSkill.Inventory.Infrastructure
{
    public class ApplicationUnitOfWork : UnitOfWorkBase<ApplicationUser, ApplicationRole, ApplicationUserClaim,
            ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim,
            ApplicationUserToken>, IApplicationUnitOfWork
    {
        #region Ctor
        public ApplicationUnitOfWork(ApplicationDbContext context,
            IEmailTrackerRepository emailTrackerRepository,
            IEmailQueueItemRepository emailQueueItemRepository,
            IFailedEmailQueueItemRepository failedEmailQueueItemRepository,
            IProductRepository productRepository,
            ICustomUserRepository userRepository)
            : base(context,
                userRepository,
                emailTrackerRepository,
                emailQueueItemRepository,
                failedEmailQueueItemRepository)
        {
            sqlUtility = new DevSkill.Inventory.Infrastructure.Utilities.SqlUtility(context.Database.GetDbConnection());
            ProductRepository = productRepository;
            UserRepository = userRepository;
        }

        #endregion

        #region Fields
        public DevSkill.Inventory.Domain.Utilities.ISqlUtility sqlUtility { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public new ICustomUserRepository UserRepository { get; private set; }
        #endregion

        #region Methods
        public async Task<(IList<Product> data, int total, int totalDisplay)> GetProductSPAsync(int pageIndex, int pageSize, string? order, ProductSearchDto search)
        {
            var procedureName = "GetProducts";

            var result = await SqlUtility.QueryWithStoredProcedureAsync<Product>(procedureName,
                new Dictionary<string, object>
                {
                    {"PageIndex", pageIndex },
                    {"PageSize", pageSize },
                    {"OrderBy", order },
                    //{"PriceFrom", search.PriceFrom },
                    //{"PriceTo", search.PriceTo },
                    {"Name", string.IsNullOrEmpty(search.Name) ? null : search.Name },
                    //{"Sku", string.IsNullOrEmpty(search.Sku) ? null : search.Sku }
                },
                new Dictionary<string, Type>
                {
                    {"Total", typeof(int) },
                    {"TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }

        #endregion
    }
}
