using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.Category;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, [FromQuery] int maxResult)
        {
            try
            {
                return Ok(await _categoryService.GetAll(new PaginationParameters { Page = page, MaxResults = maxResult }));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("getsinglebyId/{Id}")]
        public async Task<IActionResult> GetSingleById(int Id)
        {
            try
            {
                return Ok(await _categoryService.GetCategoryById(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("getsinglebyName/{Name}")]
        public async Task<IActionResult> GetSingleByName(string Name)
        {
            try
            {
                return Ok(await _categoryService.GetCategoryByName(Name));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CategoryCreate createCategory)
        {
            try
            {
                return Ok(await _categoryService.CreateCategory(createCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPatch()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(CategoryUpdate updateCategory)
        {
            try
            {
                return Ok(await _categoryService.UpdateCategory(updateCategory));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int Id)
        {
            try
            {
                return Ok(await _categoryService.DeleteCategory(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}
