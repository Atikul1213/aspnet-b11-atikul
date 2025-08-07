using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Queries
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, (IList<Sales>, int, int)>
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public GetSalesQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<(IList<Sales>, int, int)> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {

            return await _applicationUnitOfWork.SalesRepository.GetAllPagedSalesAsync(request.PageIndex, request.PageSize, request.FormatSortExpression("CustomerName", "Id"), request.SearchItem);
        }
        #endregion
    }
}
