using AutoMapper;
using Data.DTO.Cart;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        public async Task<List<CartRead>> GetShoppingCart(int userId)
        {
            List<UserCart> userCartsWithProducts = await _shoppingCartRepository.FindBy(
                e => e.UserId == userId,
                e => e.Product
            );
            
            List<CartRead> cartReadList = _mapper.Map<List<CartRead>>(userCartsWithProducts);
            Console.WriteLine( cartReadList );
            return cartReadList;
        }
    }
}
