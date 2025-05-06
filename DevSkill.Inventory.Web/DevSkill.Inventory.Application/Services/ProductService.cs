using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;

namespace DevSkill.Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public ProductService(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public async Task AddProductAsync(Product product)
        {
            await _applicationUnitOfWork.ProductRepository.AddAsync(product);
            await _applicationUnitOfWork.SaveAsync();
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetAllProductsAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return await _applicationUnitOfWork.ProductRepository.GetPagedProductAsync(pageIndex, pageSize, order, search);
        }
    }
}
