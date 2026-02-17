
using DevSkill.Core.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;

namespace DevSkill.Inventory.Domain
{
    public interface IApplicationUnitOfWork : IUnitOfWorkBase
    {
        DevSkill.Inventory.Domain.Utilities.ISqlUtility sqlUtility { get; }
        IContactUsInfoRepository ContactUsInfoRepository { get; }
        IProductRepository ProductRepository { get; }
        ICustomUserRepository UserRepository { get; }
        Task<(IList<Product> data, int total, int totalDisplay)> GetProductSPAsync(int pageIndex, int pageSize, string? order, ProductSearchDto search);
    }
}
