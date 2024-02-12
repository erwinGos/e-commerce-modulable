using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.User;
<<<<<<< Updated upstream
=======
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
>>>>>>> Stashed changes


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _AuthenticationService;

        public AuthController(IAuthenticationService authService)
        {
            _AuthenticationService = authService;
        }

<<<<<<< Updated upstream
        [HttpPost()]
        public async Task<User> SignUp(SignUpUser signUpUser)
=======
        [HttpPost("checkauth")]
        [Authorize]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpUser signUpUser)
>>>>>>> Stashed changes
        {
            User user = await _AuthenticationService.SignUp(signUpUser);
            string BearerToken = _AuthenticationService.GenerateToken(user);
            Response.Headers.Add("Set-Cookie", "Authorization=Bearer " + BearerToken + "; Path=/; HttpOnly; Secure");
            return user;
        }
    }
}
