using AutoMapper;
using Cortex.Mediator.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Queries.GetAllProductList
{
    public class GetProductListSPQueryHandler : IQueryHandler<GetProductListSPQuery, (IList<Product>, int, int)>
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public GetProductListSPQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<(IList<Product>, int, int)> Handle(GetProductListSPQuery query, CancellationToken cancellationToken)
        {
            var procedureName = "GetProducts";

            var result = await _applicationUnitOfWork.sqlUtility.QueryWithStoredProcedureAsync<Product>(procedureName,
                new Dictionary<string, object>
                {
                            {"PageIndex", query.PageIndex },
                            {"PageSize", query.PageSize },
                            {"OrderBy", query.FormatSortExpression(["Name", "CategoryName", "WholeSalePrice"]) },
                            {"MRPFrom", query.SearchItem.MRPFrom },
                            {"MRPTo", query.SearchItem.MRPTo },
                            {"StockFrom", query.SearchItem.StockFrom },
                            {"StockTo", query.SearchItem.StockTo},
                            {"Name", string.IsNullOrEmpty(query.SearchItem.Name) ? null : query.SearchItem.Name},
                            {"Category", string.IsNullOrEmpty(query.SearchItem.Category) ? null : query.SearchItem.Category },
                            {"BarCode", string.IsNullOrEmpty( query.SearchItem.BarCode) ? null : query.SearchItem.BarCode }
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
