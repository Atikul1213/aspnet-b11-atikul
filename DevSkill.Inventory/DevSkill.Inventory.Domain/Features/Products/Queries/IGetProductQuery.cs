namespace DevSkill.Inventory.Domain.Features.Products.Query
{
    public interface IGetProductQuery : IDataTable
    {
        string? Name { get; set; }
        decimal? PriceFrom { get; set; }
        decimal? PriceTo { get; set; }
        string? Sku { get; set; }
    }
}
