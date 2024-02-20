using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.Address;
using AutoMapper;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetAllAddresses()
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                bool userRole = User.FindFirst(ClaimTypes.Role).Value == "Admin" ? true : false;

                return Ok(await _addressService.GetAllAddresses(userId, userRole));

            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingleAddress(int id)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                bool userRole = User.FindFirst(ClaimTypes.Role).Value == "Admin" ? true : false;
                return Ok(await _addressService.GetSingleAddress(id, userId, userRole));

            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAddress(AddressCreate createAddress)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                Address addressToCreate = _mapper.Map<Address>(createAddress);
                addressToCreate.UserId = userId;
                return Ok(await _addressService.CreateAddress(addressToCreate));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPatch("update")]
        public async Task<IActionResult> UpdateAddress(AddressUpdate updateAddress)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                Address addressToCreate = _mapper.Map<Address>(updateAddress);
                addressToCreate.UserId = userId;
                return Ok(await _addressService.UpdateAddress(addressToCreate));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAddress(int Id)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                bool userRole = User.FindFirst(ClaimTypes.Role).Value == "Admin" ? true : false;
                return Ok(await _addressService.DeleteAddress(Id, userId, userRole));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
