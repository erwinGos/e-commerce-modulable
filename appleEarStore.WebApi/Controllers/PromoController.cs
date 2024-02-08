using Data.DTO.Promo;
using Data.Services.Contract;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromoController : ControllerBase
    {
        private readonly IPromoService _promoService;

        public PromoController(IPromoService promoService)
        {
            _promoService = promoService;
        }


        [HttpGet("getsingle/{promoCodeId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSinglePromoCode(int promoCodeId)
        {
            try
            {
                PromoCode PromoCode = await _promoService.GetSinglePromo(promoCodeId);
                return Ok(PromoCode);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<PromoCode> PromoCode = await _promoService.GetAll();
                return Ok(PromoCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreatePromo(CreatePromo createPromo)
        {
            try
            {
                PromoCode PromoCode = await _promoService.CreatePromo(createPromo);
                return Ok(PromoCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete()]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeletePromo(int promoCodeId)
        {
            try
            {
                PromoCode deletedPromo = await _promoService.Delete(promoCodeId);
                return Ok(deletedPromo);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
