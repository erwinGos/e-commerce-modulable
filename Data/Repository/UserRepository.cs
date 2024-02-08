using Microsoft.EntityFrameworkCore;
using Database;
using Database.Entities;
using Data.Repository.Contract;

namespace Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

    }
}
