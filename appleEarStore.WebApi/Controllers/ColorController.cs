using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.Color;
using AutoMapper;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        private readonly IMapper _mapper;

        public ColorController(IColorService colorService, IMapper mapper)
        {
            _colorService = colorService;
            _mapper = mapper;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAllByPage([FromQuery] int page, [FromQuery] int maxResult)
        {
            try
            {
                return Ok(await _colorService.GetColorListAsync(new PaginationParameters { Page = page, MaxResults = maxResult }));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("getsinglebyid/{Id}")]
        public async Task<IActionResult> GetAllByPage(int Id)
        {
            try
            {
                return Ok(await _colorService.GetSingleById(Id));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("getsinglebyname/{Name}")]
        public async Task<IActionResult> GetAllByPage(string Name)
        {
            try
            {
                return Ok(await _colorService.GetSingleByName(Name));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateColor(ColorCreate createColor)
        {
            try
            {
                return Ok(await _colorService.CreateColor(_mapper.Map<Color>(createColor)));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPatch()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateColor(ColorUpdate updateColor)
        {
            try
            {
                return Ok(await _colorService.UpdateColor(_mapper.Map<Color>(updateColor)));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteColor(int Id)
        {
            try
            {
                Color color = await _colorService.GetSingleById(Id) ?? throw new Exception("Cette couleur n'existe pas ou a déjà été supprimée.");
                return Ok(await _colorService.DeleteColor(color));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
