using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Brands;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Http.HttpResults;
using Data.DTO.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Protocol;
using Data.DTO.ProductDto;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IProductService _productService;

        public BrandController(IBrandService brandService, IProductService productService)
        {
            _brandService = brandService;
            _productService = productService;
        }

        [HttpGet("singleById/{Id}")]
        public async Task<IActionResult> GetBrandById(int Id)
        {
            try
            {
                Brand brand = await _brandService.GetSingleBrandById(Id);
                return Ok(brand);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("singleByName/{Name}")]
        public async Task<IActionResult> GetBrandById(string Name)
        {
            try
            {
                Brand brand = await _brandService.GetSingleBrandByName(Name);
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBrand([FromForm] BrandCreate brandCreate)
        {
            try
            {
                const int maxImageSize = 2 * 1024 * 1024;
                List<string> allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
                var memoryStream = new MemoryStream();
                if (brandCreate.Logo != null)
                {
                    await brandCreate.Logo.CopyToAsync(memoryStream);
                    string fileExtension = Path.GetExtension(brandCreate.Logo.FileName).ToLowerInvariant();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        throw new Exception("Format invalide. Seulement les formats JPG et PNG sont acceptés.");
                    }

                    if (brandCreate.Logo.Length > maxImageSize)
                    {
                        throw new Exception("La taille du fichier excède la taille maximum autorisée.");
                    }
                }

                Brand brand = new Brand()
                {
                    Name = brandCreate.Name,
                    CreatedAt = DateTime.UtcNow,
                    Logo = null
                };

                if(memoryStream != null)
                {
                    brand.Logo = memoryStream.ToArray();
                }
                await _brandService.CreateBrand(brand);

                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBrand(int BrandId)
        {
            try
            {
                Brand brand = await _brandService.GetSingleBrandById(BrandId) ?? throw new Exception("Cette marque n'existe pas, vous ne pouvez pas la supprimer.");
                List<ProductRead> productList = await _productService.GetProductListAsync(new PaginationParameters() { Brands = [brand.Name] , Colors = [], MaxResults = 1, Page = 1 });
                return Ok(await _brandService.DeleteBrand(brand, productList.Count > 0));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
