using DevSkill.Inventory.Api.Dto;
using DevSkill.Inventory.Domain.Entities;
using DevSkill.Inventory.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevSkill.Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Fields
        private readonly IProductService _productService;
        #endregion

        #region Ctor
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region Methods

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();

            return Ok(products);
        }


        [HttpGet("GetProductById/{id}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }


        // Endpoint: POST /api/products/CreateProduct
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] ProductCreateDTO productCreateDto)
        {
            var product = new Product()
            {
                Name = productCreateDto.Name,
                BarCode = productCreateDto.Sku,
                CategoryId = Guid.NewGuid(),
                CategoryName = "Default Category",
                UnitId = Guid.NewGuid(),
                BarcodeImagePath = "default-barcode.png",
                MRPPrice = productCreateDto.Price,
                WholeSalePrice = productCreateDto.Price * 0.9m,
                PurchasePrice = productCreateDto.Price * 0.8m,
                Stock = productCreateDto.Quantity,
                DamageStock = productCreateDto.Quantity,
                LowStock = productCreateDto.Quantity,
            };

            await _productService.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut]
        [Route("UpdateProduct/{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, ProductUpdateDTO productUpdateDto)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            product.Name = productUpdateDto.Name;

            await _productService.UpdateProductAsync(product);
            return Ok(product);
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id:Guid}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            await _productService.DeleteProductAsync(product.Id);

            return Ok();
        }

        [HttpGet("Search")]
        public async Task<IActionResult> EmployeeSearch([FromQuery] string? name)
        {
            var products = await _productService.GetAllProductsAsync();
            if (name is not null)
                products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(products);
        }

        #endregion
    }
}
