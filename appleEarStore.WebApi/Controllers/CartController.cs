using Data.DTO.Cart;
using Data.Services.Contract;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("self")]
        public async Task<IActionResult> GetShoppingCart()
        {
            List<CartRead> userCart = await _cartService.GetShoppingCart();
            return Ok(userCart);
        }
    }
}
