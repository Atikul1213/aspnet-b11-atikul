namespace DevSkill.Inventory.Application.Features.Products.Queries.PrevQueries
{
    //public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, (IList<Product>, int, int)>
    //{
    //    #region Fields

    //    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    //    private readonly IMapper _mapper;

    //    #endregion

    //    #region Ctor
    //    public GetAllProductQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
    //        IMapper mapper)
    //    {
    //        _applicationUnitOfWork = applicationUnitOfWork;
    //        _mapper = mapper;
    //    }
    //    #endregion

    //    #region Methods
    //    public async Task<(IList<Product>, int, int)> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    //    {
    //        var procedureName = "GetProducts";

    //        var result = await _applicationUnitOfWork.sqlUtility.QueryWithStoredProcedureAsync<Product>(procedureName,
    //            new Dictionary<string, object>
    //            {
    //                {"PageIndex", request.PageIndex },
    //                {"PageSize", request.PageSize },
    //                {"OrderBy", request.FormatSortExpression(["Name", "CategoryName", "WholeSalePrice"]) },
    //                {"MRPFrom", request.SearchItem.MRPFrom },
    //                {"MRPTo", request.SearchItem.MRPTo },
    //                {"StockFrom", request.SearchItem.StockFrom },
    //                {"StockTo", request.SearchItem.StockTo},
    //                {"Name", string.IsNullOrEmpty(request.SearchItem.Name) ? null : request.SearchItem.Name},
    //                {"Category", string.IsNullOrEmpty(request.SearchItem.Category) ? null : request.SearchItem.Category },
    //                {"BarCode", string.IsNullOrEmpty( request.SearchItem.BarCode) ? null : request.SearchItem.BarCode }
    //            },
    //            new Dictionary<string, Type>
    //            {
    //                {"Total", typeof(int) },
    //                {"TotalDisplay", typeof(int) },
    //            });

    //        return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
    //    }
    //    #endregion
    //}
}
