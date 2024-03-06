using Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Data.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Data.DTO.Pagination;
using System.Security.Claims;
using Data.DTO.Order;

namespace appleEarStore.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _OrderService;


        public OrderController(IOrderService orderService)
        {
            _OrderService = orderService;
        }

        [HttpGet("/getsingle/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetOrderById(int Id)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                PaginationParameters parameters = new PaginationParameters()
                {
                    isAdmin = User.FindFirst(ClaimTypes.Role).Value == "Admin"
                };
                Order order = await _OrderService.GetSingleOrder(Id, userId, parameters);
                return Ok(order);
            } catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetMyOrders([FromQuery] int page, [FromQuery] int maxResult)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);

                PaginationParameters parameters = new PaginationParameters()
                {
                    Page = page,
                    MaxResults = maxResult
                };

                PaginationOrder orders = await _OrderService.GetUserOrderList(userId, parameters);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("admin/getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] int page, [FromQuery] int maxResult)
        {
            try
            {
                PaginationParameters parameters = new PaginationParameters()
                {
                    Page = page,
                    MaxResults = maxResult
                };

                List<Order> orders = await _OrderService.GetAllUsersOrders(parameters);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateOrder(CreateOrder createOrder)
        {
            try
            {
                int userId = Int32.Parse(User.FindFirst("UserId").Value);
                OrderRead order = await _OrderService.CreateOrder(createOrder, userId);

                return Ok(order);

            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
