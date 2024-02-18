
using Database.Entities;

namespace Data.Repository.Contract
{
    public interface IShoppingCartRepository: IGenericRepository<UserCart>
    {
        new Task<UserCart> Update(UserCart element);
    }
}
