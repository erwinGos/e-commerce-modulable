using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using Stripe.Climate;
using AutoMapper;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStripeService _stripeService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService productService, IStripeService stripeService)
        {
            _stripeService = stripeService;
            _productService = productService;
            _mapper = mapper;
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
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            try
            {
                ProductRead createdProduct = await _productService.CreateProduct(createProduct);
                Stripe.Product stripeProduct = await _stripeService.CreateProduct(createProduct);

                createdProduct.StripeProductId = stripeProduct.Id;
                await _productService.SaveUpDatabase(_mapper.Map<Database.Entities.Product>(createdProduct));
                return Ok(createdProduct);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPatch()]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            ProductRead updatedProduct = await _productService.UpdateProduct(_mapper.Map<Database.Entities.Product>(updateProduct));
            return Ok(updatedProduct);
        }

        [Authorize]
        [HttpDelete()]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                ProductRead updatedProduct = await _productService.DeactivateProduct(productId);
                Task<String> removeProductReturn = _stripeService.RemoveProduct(updatedProduct);
                return Ok(new { Product = updatedProduct , Message = removeProductReturn });
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
