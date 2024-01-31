using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;


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
        public string[] GetProductListAsync([FromQuery] int page, [FromQuery] int max_items, [FromQuery] string brands)
        {
            var brandList = brands?.Split(new char[] { '|', ',' }, StringSplitOptions.RemoveEmptyEntries);
            return brandList;
        }
    }
}
