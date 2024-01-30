using Data.DTO.User;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IAuthenticationService
    {
        public Task<User> SignUp(SignUpUser signUpUser);

        public string GenerateToken(User user);
    }
}
