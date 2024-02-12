using Data.DTO.Order;
using Data.DTO.Pagination;
using Data.DTO.Product;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;
using System.Collections.Generic;

namespace Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPromoRepository _promoRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository, IPromoRepository promoRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _promoRepository = promoRepository;
            _productRepository = productRepository;
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
