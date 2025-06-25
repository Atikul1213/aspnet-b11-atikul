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
            return await _applicationUnitOfWork.CustomerRepository.GetPagedCustomersAsync(request.PageIndex, request.PageSize, request?.FormatSortExpression("Id"), request.Search);
        }
        //public async Task<(IList<Customer> data, int total, int totalDisplay)> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        //{
        //    var customerSearchDto = _mapper.Map<CustomerSearchDto>(request);

        //    return await _applicationUnitOfWork.CustomerRepository.GetCQRSPagedCustomersAsync(request.PageIndex, request.PageSize, request.FormatSortExpression("Name", "Sku", "Price", "Id"), customerSearchDto);
        //}
        #endregion
    }
}
