using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Microsoft.AspNetCore.Authorization;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;
        }

        [HttpGet()]
        [Authorize()]
        public async Task<IActionResult> GetSelfUser()
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                User user = await _UserService.GetUserById(userId);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
