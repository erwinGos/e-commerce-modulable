using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Data.DTO.User;


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

        [HttpPost()]
        public async Task<User> SignUp(SignUpUser signUpUser)
        {
            User user = await _AuthenticationService.SignUp(signUpUser);
            string BearerToken = _AuthenticationService.GenerateToken(user);
            Response.Headers.Add("Set-Cookie", "Authorization=Bearer " + BearerToken + "; Path=/; HttpOnly; Secure");
            return user;
        }
    }
}
