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
using Data.Managers;
using System.Drawing.Drawing2D;
using Data;


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

        [HttpGet("getall")]
        public async Task<IActionResult> GetAllBrand([FromQuery] int page, [FromQuery] int maxResult)
        {
            try
            {
                return Ok(await _brandService.GetAllBrands(new PaginationParameters { Page = page, MaxResults = maxResult }));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
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
                Brand checkIfExists = await _brandService.GetSingleBrandByName(brandCreate.Name) != null ? throw new Exception("Une marque avec ce nom existe déjà.") : null;
                Brand brand = new Brand()
                {
                    Name = brandCreate.Name,
                    UpdatedAt = DateTime.UtcNow,
                    Logo = null
                };
                
                if(brandCreate.Logo != null)
                {
                    byte[] memoryStream = await ImageManager.ConvertImage(brandCreate.Logo, [".png", ".jpg", ".jpeg"], 2);
                    if (memoryStream != null)
                    {
                        brand.Logo = memoryStream;
                    }
                }
                return Ok(await _brandService.CreateBrand(brand));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPatch("")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBrand(UpdateBrand updateBrand)
        {
            try
            {
                Brand brand = new Brand()
                {
                    Id = updateBrand.Id,
                    Name = updateBrand.Name,
                    Logo = null,
                    UpdatedAt = DateTime.Now
                };

                if (updateBrand.Logo != null)
                {
                    byte[] memoryStream = await ImageManager.ConvertImage(updateBrand.Logo, [".png", ".jpg", ".jpeg"], 2);
                    if (memoryStream != null)
                    {
                        brand.Logo = memoryStream;
                    }
                }

                return Ok(await _brandService.UpdateBrand(brand));
            } catch (Exception ex)
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
                PaginationProduct productList = await _productService.GetProductListAsync(new PaginationParameters() { Brands = [brand.Name] , Colors = [], MaxResults = 1, Page = 1 });
                return Ok(await _brandService.DeleteBrand(brand, productList.Products.Count > 0));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
