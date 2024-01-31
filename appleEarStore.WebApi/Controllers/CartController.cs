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
            int adminId = Int32.Parse(User.FindFirst("UserId").Value);
            List<CartRead> userCart = await _cartService.GetShoppingCart(adminId);
            return Ok(userCart);
        }
    }
}
