using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Database.Entities;

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
        public async Task<List<Product>> GetProductList([FromQuery] int page, [FromQuery] int maxResult, [FromQuery] string? brands, [FromQuery] string? colors)
        {
            string[] brandList = brands?.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            string[] colorList = colors?.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
            List<Product> productList = await _productService.GetProductListAsync(new PaginationParameters() { Brands = brandList, MaxResults = maxResult, Page = page, Colors = colorList });
            return productList;
        }
    }
}
