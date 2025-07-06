using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Repositories;
using System.Linq.Expressions;

namespace DevSkill.Inventory.Infrastructure.Repositories
{
    public class SalesRepository : Repository<Sales, Guid>, ISalesRepository
    {
        public SalesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<(IList<Sales> data, int total, int totalDisplay)> GetAllPagedSalesAsync(int pageIndex, int pageSize, string? order, SalesSearchDto search)
        {
            Expression<Func<Sales, bool>> filter = null;
            if (search != null)
            {
                //filter = x =>
                //    //(string.IsNullOrEmpty(search.) || x.Name.Contains(search.Name.ToLowerInvariant())) &&
                //    //(string.IsNullOrEmpty(search.BarCode) || x.BarCode.ToLower().Contains(search.BarCode.ToLower())) &&
                //    //(string.IsNullOrEmpty(search.Category) || x.CategoryName.ToLower().Contains(search.Category.ToLower())) &&
                //    //(!search.MRPFrom.HasValue || x.MRPPrice >= search.MRPFrom.Value) &&
                //    //(!search.MRPTo.HasValue || x.MRPPrice <= search.MRPTo.Value) &&
                //    //(!search.StockFrom.HasValue || x.Stock >= search.StockFrom.Value) &&
                //    //(!search.StockTo.HasValue || x.Stock <= search.StockTo.Value);
            }
            return await GetDynamicAsync(filter, order, null, pageIndex, pageSize, true);
        }
    }
}
