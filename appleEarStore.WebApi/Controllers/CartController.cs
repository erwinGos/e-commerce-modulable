using Data.DTO.Cart;
using Data.Services.Contract;
using Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly IShoppingCartService _cartService;

        public CartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        [HttpGet("self")]
        public async Task<IActionResult> GetShoppingCart()
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                List<CartRead> userCart = await _cartService.GetShoppingCart(userId);
                return Ok(userCart);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpPost("addtocart")]
        public async Task<IActionResult> AddProductToShoppingCart(AddToCart addToCart)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                CartRead freshCartProduct = await _cartService.AddProductToShoppingCart(addToCart, userId);
                return Ok(freshCartProduct);
            } catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("removefromCart")]
        public async Task<IActionResult> RemoveProductFromShoppingCart(int userCartId)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                CartRead freshCartProduct = await _cartService.RemoveProductFromShoppingCart(userCartId, userId);
                return Ok(freshCartProduct);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
