using AutoMapper;
using Data.DTO.User;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;


namespace Data.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _UserRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> GetUserById(int Id)
        {
            User users = await _UserRepository.FindSingleBy(u => u.Id == Id);
            return users;
        }

        public async Task<User> UpdateUser(UpdateUser updateUser, User user)
        {
            try
            {
                user.Email = updateUser.Email ?? user.Email;
                user.Password = updateUser.Password ?? user.Password;
                user.Name = updateUser.Name ?? user.Name;
                user.LastName = updateUser.LastName ?? user.LastName;
                return await _UserRepository.Update(user);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
