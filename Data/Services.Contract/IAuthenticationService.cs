using Data.DTO.User;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IAuthenticationService
    {
        public string GenerateToken(User user);
        public Task<User> SignUp(SignUpUser signUpUser);
        public Task<User> SignIn(SignInUser signInUser);

    }
}
