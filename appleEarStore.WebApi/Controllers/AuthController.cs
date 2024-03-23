using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.User;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;


namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _AuthenticationService;

        private readonly IConfiguration _configuration;

        public AuthController(IAuthenticationService authService, IConfiguration configuration)
        {
            _AuthenticationService = authService;
            _configuration = configuration;
        }



        [Authorize]
        [HttpGet("checkAuth")]
        public async Task<IActionResult> CheckAuth()
        {
            return Ok(true);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpUser signUpUser)
        {
            User user = await _AuthenticationService.SignUp(signUpUser);
            string BearerToken = _AuthenticationService.GenerateToken(user);

            DateTime time = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiresInHours"]));
            string timeToString = time.ToString("R", CultureInfo.InvariantCulture);

            Response.Headers.Add("Set-Cookie", "auth_token=Bearer " + BearerToken + "; Path=/; Secure; Expires=" + timeToString);
            return Ok(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInUser signInUser)
        {
            try
            {
                User user = await _AuthenticationService.SignIn(signInUser);
                if(user.IsDeactivated) {
                    return BadRequest(new { message = "Votre compte est actuellement désactivé." });
                }

                if(user.IsBanned) {
                    return BadRequest(new { message = "Votre compte est actuellement banni." });
                }
                
                string BearerToken = _AuthenticationService.GenerateToken(user);

                DateTime time = DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpiresInHours"]));
                string timeToString = time.ToString("R", CultureInfo.InvariantCulture);

                Response.Headers.Add("Set-Cookie", "auth_token=Bearer " + BearerToken + "; Path=/; Secure; Expires=" + timeToString);
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            DateTime time = DateTime.UtcNow.AddMicroseconds(1000);
            string timeToString = time.ToString("R", CultureInfo.InvariantCulture);
            Response.Headers.Add("Set-Cookie", "auth_token=Bearer ; Path=/; Secure;Expires=" + timeToString);
            return Ok();
        }
    }
}
