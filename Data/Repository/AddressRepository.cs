using Data.Repository.Contract;
using Database;
using Database.Entities;

namespace Data.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(DatabaseContext context) : base(context)
        {
        }

    }
}
