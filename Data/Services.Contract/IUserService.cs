using Data.DTO.User;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IUserService
    {
        public Task<User> GetUserById(int Id);

        public Task<User> UpdateUser(UpdateUser updateUser, User user);
    }
}
