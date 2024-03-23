using Data.DTO.Cart;

namespace Data.Services.Contract
{
    public interface IShoppingCartService
    {
        public Task<List<CartRead>> GetShoppingCart(int userId);

        public Task<CartRead> AddProductToShoppingCart(AddToCart addToCart, int UserId);

        public Task<CartRead> RemoveProductFromShoppingCart(int userCartId, int userId);

        public Task<string> ClearShoppingCartAfterOrder(int userId);
    }
}
