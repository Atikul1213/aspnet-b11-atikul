using AutoMapper;
using Cortex.Mediator.Queries;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IQueryHandler<GetProductListQuery, (IList<Product>, int, int)>
    {
        #region Fields

        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public GetProductListQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
            IMapper mapper)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        public async Task<(IList<Product>, int, int)> Handle(GetProductListQuery query, CancellationToken cancellationToken)
        {
            return await _applicationUnitOfWork.ProductRepository.GetAllPagedProductAsync(query.PageIndex, query.PageSize, query.FormatSortExpression("Name", "Id"), query.SearchItem);
        }
        #endregion
    }
}
