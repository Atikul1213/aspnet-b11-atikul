using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.BalanceTransfers.Queries
{
    public class GetBalanceTransferListQueryHandler : IRequestHandler<GetBalanceTransferListQuery, (IList<BalanceTransfer> data, int total, int totalDisplay)>
    {
        #region Fields
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public GetBalanceTransferListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods

        public async Task<(IList<BalanceTransfer> data, int total, int totalDisplay)> Handle(GetBalanceTransferListQuery request, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.BalanceTransferRepository.GetPagedBalanceTransferAsync(request.PageIndex, request.PageSize, request?.FormatSortExpression("Id"));
        }
        //public async Task<(IList<Customer> data, int total, int totalDisplay)> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
        //{
        //    var customerSearchDto = _mapper.Map<CustomerSearchDto>(request);

        //    return await _applicationUnitOfWork.CustomerRepository.GetCQRSPagedCustomersAsync(request.PageIndex, request.PageSize, request.FormatSortExpression("Name", "Sku", "Price", "Id"), customerSearchDto);
        //}
        #endregion
    }
}
