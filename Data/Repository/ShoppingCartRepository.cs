using Data.Repository.Contract;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ShoppingCartRepository : GenericRepository<UserCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(DatabaseContext context) : base(context)
        {
        }


    }
}
