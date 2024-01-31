using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;


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
        public async Task<User> GetUserByEmail(string email)
        {
            User user = await _UserService.GetUserByEmailAsync(email);
            return user;
        }
    }
}
