using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.Pagination;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.Voucher;
using AutoMapper;
using Data.Managers;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;
        private readonly IMapper _mapper;

        public VoucherController(IVoucherService voucherService, IMapper mapper)
        {
            _voucherService = voucherService;
            _mapper = mapper;
        }

        [HttpGet()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVoucherPagination([FromQuery] int page, [FromQuery] int maxResult)
        {
            try
            {
                return Ok(await _voucherService.GetAllVouchers(new PaginationParameters { Page = page, MaxResults = maxResult }));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{Id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVoucher(int Id)
        {
            try
            {
                return Ok(await _voucherService.DeleteVoucher(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVoucher(VoucherCreate voucherCreate)
        {
            try
            {
                Vouchers newVoucher = _mapper.Map<Vouchers>(voucherCreate);
                newVoucher.Code = VoucherManager.GenerateVoucher();
                return Ok(await _voucherService.CreateVoucher(newVoucher));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost("usevoucher")]
        [Authorize]
        public async Task<IActionResult> UseVoucher(string code)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                return Ok(await _voucherService.UseVoucher(code, userId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
