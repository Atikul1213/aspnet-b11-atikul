using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Api.Dto
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product Sku is required")]
        public string Sku { get; set; }
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be a Negative value")]
        public int Quantity { get; set; }
    }
}
