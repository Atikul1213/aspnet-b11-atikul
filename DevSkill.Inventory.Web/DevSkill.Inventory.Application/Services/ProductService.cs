using DevSkill.Inventory.Application.Exceptions;
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
            if (!await _applicationUnitOfWork.ProductRepository.CheckSkuDuplicateAsync(product.Sku))
            {
                await _applicationUnitOfWork.ProductRepository.AddAsync(product);
                await _applicationUnitOfWork.SaveAsync();
            }
            else
            {
                throw new DuplicateProductSkuException();
            }
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetAllProductsAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            return await _applicationUnitOfWork.ProductRepository.GetPagedProductAsync(pageIndex, pageSize, order, search);
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _applicationUnitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task DeleteProductAsync(Guid id)
        {
            await _applicationUnitOfWork.ProductRepository.RemoveAsync(id);
            await _applicationUnitOfWork.SaveAsync();
        }
    }
}
