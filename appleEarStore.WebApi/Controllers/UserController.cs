using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.User;
using Data.Managers;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _UserService;
        private readonly IAuthenticationService _AuthenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _UserService = userService;
            _AuthenticationService = authenticationService;
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

        [HttpPatch()]
        [Authorize()]
        public async Task<IActionResult> UpdateUser(UpdateUser updateUser)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                User checkUser = await _UserService.GetUserById(userId);
                User user = await _AuthenticationService.SignIn(new SignInUser { Email = checkUser.Email, Password = updateUser.OldPassword}) ?? throw new Exception("Mot de passe incorrect.");
                if(updateUser.Password != null)
                {
                    updateUser.Password = IdentityManager.HashPassword(updateUser.Password);
                }
                return Ok(await _UserService.UpdateUser(updateUser, user));
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
