using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Data.DTO.Product;
using Microsoft.AspNetCore.Authorization;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList([FromQuery] int page, [FromQuery] int maxResult, [FromQuery] string? brands, [FromQuery] string? colors)
        {
            try
            {
                string[] brandList = brands?.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                string[] colorList = colors?.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                List<ProductRead> productList = await _productService.GetProductListAsync(new PaginationParameters() { Brands = brandList, MaxResults = maxResult, Page = page, Colors = colorList });
                return Ok(productList);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct(int Id)
        {
            try
            {
                ProductRead product = await _productService.FindOne(Id);
                return Ok(product);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> UpdateProduct(CreateProduct createProduct)
        {
            ProductRead updatedProduct = await _productService.CreateProduct(createProduct);
            return Ok(updatedProduct);
        }

        [Authorize]
        [HttpPatch()]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            ProductRead updatedProduct = await _productService.UpdateProduct(updateProduct);
            return Ok(updatedProduct);
        }

        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            ProductRead updatedProduct = await _productService.DeactivateProduct(productId);
            return Ok(updatedProduct);
        }
    }
}
