using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Products.Query;
using DevSkill.Inventory.Domain.Repositories;
using System.Linq.Expressions;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetAllPagedProductAsync(int pageIndex, int pageSize, string? order, ProductSearchDto search)
        {
            Expression<Func<Product, bool>> filter = null;
            if (search != null)
            {
                filter = x =>
                    (string.IsNullOrEmpty(search.Name) || x.Name.Contains(search.Name.ToLowerInvariant())) &&
                    (string.IsNullOrEmpty(search.BarCode) || x.BarCode.ToLower().Contains(search.BarCode.ToLower())) &&
                    (string.IsNullOrEmpty(search.Category) || x.CategoryName.ToLower().Contains(search.Category.ToLower())) &&
                    (!search.MRPFrom.HasValue || x.MRPPrice >= search.MRPFrom.Value) &&
                    (!search.MRPTo.HasValue || x.MRPPrice <= search.MRPTo.Value) &&
                    (!search.StockFrom.HasValue || x.Stock >= search.StockFrom.Value) &&
                    (!search.StockTo.HasValue || x.Stock <= search.StockTo.Value);
            }
            return await GetDynamicAsync(filter, order, null, pageIndex, pageSize, true);
        }

        public async Task<(IList<Product> data, int total, int totalDisplay)> GetPagedProductAsync(int pageIndex, int pageSize, string? order, DataTablesSearch search)
        {
            if (string.IsNullOrEmpty(search.Value))
                return await GetDynamicAsync(null, order, null, pageIndex, pageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(search.Value), order, null, pageIndex, pageSize, true);
        }
        public async Task<(IList<Product> data, int total, int totalDisplay)> GetCQRSPagedProductAsync(IGetProductQuery request)
        {
            if (string.IsNullOrEmpty(request.Search.Value))
                return await GetDynamicAsync(null, request.FormatSortExpression("Id", "Name"), null, request.PageIndex, request.PageSize, true);
            else
                return await GetDynamicAsync(x => x.Name.Contains(request.Search.Value) || x.BarCode.Contains(request.Search.Value), request.FormatSortExpression("Id", "Name"), null, request.PageIndex, request.PageSize, true);
        }

        public async Task<bool> CheckBarCodeDuplicateAsync(string barCode, Guid? id = null)
        {
            if (id.HasValue)
            {
                return await GetCountAsync(x => x.Id != id && x.BarCode == barCode) > 0;
            }

            return await GetCountAsync(x => x.BarCode == barCode) > 0;
        }
    }
}
