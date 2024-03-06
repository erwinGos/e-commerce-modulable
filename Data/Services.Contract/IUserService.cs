using Database.Entities;

namespace Data.Services.Contract
{
    public interface IUserService
    {
        public Task<User> GetUserById(int Id);

    }
}
