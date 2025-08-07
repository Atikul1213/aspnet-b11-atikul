using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Customers.Queries
{
    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, (IList<Customer> data, int total, int totalDisplay)>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public GetCustomerListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<(IList<Customer> data, int total, int totalDisplay)> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        {
            var procedureName = "GetCustomers";

            var result = await _applicationUnitOfWork.sqlUtility.QueryWithStoredProcedureAsync<Customer>(procedureName,
                new Dictionary<string, object>
                {
                    {"PageIndex", request.PageIndex },
                    {"PageSize", request.PageSize },
                    {"OrderBy", request.FormatSortExpression(["Name", "CompanyName", "MobileNumber"]) },
                    {"BalanceFrom", request.SearchItem.BalanceFrom },
                    {"BalanceTo", request.SearchItem.BalanceTo },
                    {"Name", string.IsNullOrEmpty(request.SearchItem.Name) ? null : request.SearchItem.Name},
                    {"Email", string.IsNullOrEmpty(request.SearchItem.Email) ? null : request.SearchItem.Email },
                    {"CompanyName", string.IsNullOrEmpty( request.SearchItem.CompanyName) ? null : request.SearchItem.CompanyName },
                    {"MobileNumber", string.IsNullOrEmpty( request.SearchItem.MobileNumber) ? null : request.SearchItem.MobileNumber }
                },
                new Dictionary<string, Type>
                {
                    {"Total", typeof(int) },
                    {"TotalDisplay", typeof(int) },
                });

            return (result.result, (int)result.outValues["Total"], (int)result.outValues["TotalDisplay"]);
        }
        //public async Task<(IList<Customer> data, int total, int totalDisplay)> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        //{
        //    var customerSearchDto = _mapper.Map<CustomerSearchDto>(request);

        //    return await _applicationUnitOfWork.CustomerRepository.GetCQRSPagedCustomersAsync(request.PageIndex, request.PageSize, request.FormatSortExpression("Name", "Sku", "Price", "Id"), customerSearchDto);
        //}
        #endregion
    }
}
