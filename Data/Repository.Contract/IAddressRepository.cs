using Database.Entities;

namespace Data.Repository.Contract
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        new Task<Address> Update(Address element);
    }
}
