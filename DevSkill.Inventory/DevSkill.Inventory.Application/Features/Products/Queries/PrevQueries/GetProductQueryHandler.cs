namespace DevSkill.Inventory.Application.Features.Products.Queries.PrevQueries
{
    //public class GetProductQueryHandler : IRequestHandler<GetProductQuery, (IList<Product>, int, int)>
    //{
    //    #region Fields

    //    private readonly IApplicationUnitOfWork _applicationUnitOfWork;
    //    private readonly IMapper _mapper;

    //    #endregion

    //    #region Ctor
    //    public GetProductQueryHandler(IApplicationUnitOfWork applicationUnitOfWork,
    //        IMapper mapper)
    //    {
    //        _applicationUnitOfWork = applicationUnitOfWork;
    //        _mapper = mapper;
    //    }
    //    #endregion

    //    #region Methods
    //    public async Task<(IList<Product>, int, int)> Handle(GetProductQuery request, CancellationToken cancellationToken)
    //    {

    //        return await _applicationUnitOfWork.ProductRepository.GetAllPagedProductAsync(request.PageIndex, request.PageSize, request.FormatSortExpression("Name", "Id"), request.SearchItem);
    //    }
    //    #endregion
    //}
}
