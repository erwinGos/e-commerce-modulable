using Data.DTO.Cart;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IShoppingCartService
    {
        public Task<List<CartRead>> GetShoppingCart(int userId);
    }
}
