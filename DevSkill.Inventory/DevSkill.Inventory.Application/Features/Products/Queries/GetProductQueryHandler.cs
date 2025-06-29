using AutoMapper;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using MediatR;

namespace DevSkill.Inventory.Application.Features.Products.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, (IList<Product>, int, int)>
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public GetProductQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<(IList<Product>, int, int)> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var productSearchDto = _mapper.Map<ProductSearchDto>(request);

            return await _applicationUnitOfWork.GetProductSPAsync(request.PageIndex, request.PageSize, request.FormatSortExpression("Name", "Id"), productSearchDto);
        }
        #endregion
    }
}
