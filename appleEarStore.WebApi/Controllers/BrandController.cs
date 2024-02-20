using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Brands;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
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
        public async Task<IActionResult> CreateBrand(Brand brandCreate)
        {
            try
            {
                Brand brand = await _brandService.CreateBrand(brandCreate);
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
