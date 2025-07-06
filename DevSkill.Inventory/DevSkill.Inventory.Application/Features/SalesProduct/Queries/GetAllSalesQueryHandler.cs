using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Queries
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, (IList<Sales>, int, int)>
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public GetAllSalesQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<(IList<Sales>, int, int)> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var procedureName = "GetSaless";

            var result = await _applicationUnitOfWork.sqlUtility.QueryWithStoredProcedureAsync<Sales>(procedureName,
                new Dictionary<string, object>
                {
                    {"PageIndex", request.PageIndex },
                    {"PageSize", request.PageSize },
                    {"OrderBy", request.FormatSortExpression(["Name", "CategoryName", "WholeSalePrice"]) },
                    //{"MRPFrom", request.SearchItem.MRPFrom },
                    //{"MRPTo", request.SearchItem.MRPTo },
                    //{"StockFrom", request.SearchItem.StockFrom },
                    //{"StockTo", request.SearchItem.StockTo},
                    //{"Name", string.IsNullOrEmpty(request.SearchItem.Name) ? null : request.SearchItem.Name},
                    //{"Category", string.IsNullOrEmpty(request.SearchItem.Category) ? null : request.SearchItem.Category },
                    //{"BarCode", string.IsNullOrEmpty( request.SearchItem.BarCode) ? null : request.SearchItem.BarCode }
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
