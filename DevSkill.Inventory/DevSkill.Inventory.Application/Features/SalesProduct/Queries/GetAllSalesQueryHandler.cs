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
            var procedureName = "GetSalesProduct";

            var result = await _applicationUnitOfWork.sqlUtility.QueryWithStoredProcedureAsync<Sales>(procedureName,
                new Dictionary<string, object>
                {
                    {"PageIndex", request.PageIndex },
                    {"PageSize", request.PageSize },
                    {"OrderBy", request.FormatSortExpression(["SaleDate", "CustomerName", "TotalAmount"]) },
                    //{"DateFrom", request.SearchItem.DateFrom },
                    //{"DateTo", request.SearchItem.DateTo },
                    //{"TotalFrom", request.SearchItem.TotalFrom },
                    //{"TotalTo", request.SearchItem.TotalTo},
                    //{"StatusId", request.SearchItem.StatusId},
                    //{"CustomerName", string.IsNullOrEmpty(request.SearchItem.CustomerName) ? null : request.SearchItem.CustomerName},
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
