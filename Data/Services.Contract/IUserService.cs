using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Contract
{
    public interface IUserService
    {
        public Task<User> GetUserByEmailAsync(string email);

    }
}
