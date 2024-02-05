

using Data.DTO.Pagination;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetSingleOrder(int orderId, int userId, PaginationParameters parameters)
        {
            try
            {
                Order order = await _orderRepository.FindSingleBy(o => o.Id == orderId && (o.UserId == userId || parameters.isAdmin)) ?? throw new Exception("Cette commande n'existe pas ou ne vous appartient pas.");
                return order;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetUserOrderList(int UserId, PaginationParameters parameters)
        {
            try
            {
                List<Order> orders = await _orderRepository.GetOrderListAsync(UserId, parameters);
                return orders;
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetAllUsersOrders(PaginationParameters parameters)
        {
            try
            {
                List<Order> orders = await _orderRepository.GetAllOrdersPagination(parameters);
                return orders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
