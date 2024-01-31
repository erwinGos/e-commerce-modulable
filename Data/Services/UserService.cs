using Data.Repository;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;


namespace Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;

        public UserService(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            User users = await _UserRepository.FindSingleBy(u => u.Email == email);
            return users;
        }
    }
}
