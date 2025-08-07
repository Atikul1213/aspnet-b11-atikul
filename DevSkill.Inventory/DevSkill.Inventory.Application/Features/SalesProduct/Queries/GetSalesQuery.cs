using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Features.Sales.Queries;
using MediatR;

namespace DevSkill.Inventory.Application.Features.SalesProduct.Queries
{
    public class GetSalesQuery : DataTables, IRequest<(IList<Sales>, int, int)>, IGetSalesQuery
    {
        public SalesSearchDto SearchItem { get; set; }
    }
}
