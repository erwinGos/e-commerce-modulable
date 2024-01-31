using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.User;
using System.Globalization;
using Microsoft.AspNetCore.Http.HttpResults;


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

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpUser signUpUser)
        {
            User user = await _AuthenticationService.SignUp(signUpUser);
            string BearerToken = _AuthenticationService.GenerateToken(user);
            Response.Headers.Add("Set-Cookie", "auth_token=Bearer " + BearerToken + "; Path=/; HttpOnly; Secure");
            return Ok(user);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInUser signInUser)
        {
            try
            {
                User user = await _AuthenticationService.SignIn(signInUser);
                string BearerToken = _AuthenticationService.GenerateToken(user);
                Response.Headers.Add("Set-Cookie", "auth_token=Bearer " + BearerToken + "; Path=/; HttpOnly; Secure");
                return Ok(user);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("signout")]
        public IActionResult SignOut()
        {
            DateTime time = DateTime.UtcNow.AddMicroseconds(1000);
            string timeToString = time.ToString("R", CultureInfo.InvariantCulture);
            Response.Headers.Add("Set-Cookie", "auth_token=Bearer ; Path=/; HttpOnly; Secure;Expires=" + timeToString);
            return Ok();
        }
    }
}
