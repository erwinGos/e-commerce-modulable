using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using Stripe.Climate;
using AutoMapper;
using System.Drawing.Drawing2D;
using Data.Services;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService productService, IBrandService brandService)
        {
            _productService = productService;
            _brandService = brandService;
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

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(CreateProduct createProduct)
        {
            try
            {
                ProductRead createdProduct = await _productService.CreateProduct(createProduct);
                return Ok(createdProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch()]
        public async Task<IActionResult> UpdateProduct(UpdateProduct updateProduct)
        {
            try
            {
                ProductRead updatedProduct = await _productService.UpdateProduct(_mapper.Map<Database.Entities.Product>(updateProduct));
                return Ok(updatedProduct);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete()]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                ProductRead updatedProduct = await _productService.DeactivateProduct(productId);
                return Ok(updatedProduct);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("groupchangebrand")]
        public async Task<IActionResult> ChangeBrandFromGroupedProduct(int newBrandId, int oldBrandId)
        {
            try
            {
                Brand brand = await _brandService.GetSingleBrandById(oldBrandId) ?? throw new Exception("Cette marque n'existe pas, elle n'a donc pas de produit corrélés.");
                List<Database.Entities.Product> productList = await _productService.GetAllProductsByBrand(brand.Id);
                List<Database.Entities.Product> products = await _productService.ChangeBrandFromGroupedProduct(productList, newBrandId);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
