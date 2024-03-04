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

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<CartRead>> GetShoppingCart(int userId)
        {
            try
            {
                List<UserCart> userCartsWithProducts = await _shoppingCartRepository.FindBy(
                e => e.UserId == userId,
                e => e.Product.ProductImages.Take(1),
                e => e.Product.Brand
            );

                List<CartRead> cartReadList = _mapper.Map<List<CartRead>>(userCartsWithProducts);
                return cartReadList;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartRead> AddProductToShoppingCart(AddToCart addToCart, int userId)
        {
            try
            {
                Product CheckIfExists = await _productRepository.FindSingleBy(p => p.Id == addToCart.ProductId) ?? throw new Exception("Produit non trouvé.");
                UserCart existingUserCartItem = await _shoppingCartRepository.FindSingleBy(uc => uc.ProductId == addToCart.ProductId && uc.UserId == userId);
                if(existingUserCartItem == null)
                {
                    UserCart newUserCartItem = _mapper.Map<UserCart>(addToCart);
                    newUserCartItem.UserId = userId;
                    newUserCartItem.ColorName = addToCart.ColorName;
                    UserCart freshlyCreatedUserCartItem = await _shoppingCartRepository.Insert(newUserCartItem).ConfigureAwait(false);
                    UserCart freshlyCreatedUserCartItemWithImage = await _shoppingCartRepository.FindSingleBy(usercart => usercart.Id == freshlyCreatedUserCartItem.Id, 
                        usercart => usercart.Product.ProductImages.Take(1)
                        );
                    return _mapper.Map<CartRead>(freshlyCreatedUserCartItemWithImage);
                } else
                {
                    existingUserCartItem.Quantity = addToCart.Quantity;
                    UserCart freshlyUpdatedUserCartItem = await _shoppingCartRepository.Update(existingUserCartItem).ConfigureAwait(false);
                    return _mapper.Map<CartRead>(freshlyUpdatedUserCartItem);
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CartRead> RemoveProductFromShoppingCart(int userCartId, int userId)
        {
            try
            {
                UserCart existingUserCartItem = await _shoppingCartRepository.FindSingleBy(uc => uc.Id == userCartId && uc.UserId == userId);
                if( existingUserCartItem == null)
                {
                    throw new Exception("Une erreur s'est produite.");
                }
                UserCart deletedUserCart = await _shoppingCartRepository.Delete(existingUserCartItem);
                return _mapper.Map<CartRead>(deletedUserCart);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
