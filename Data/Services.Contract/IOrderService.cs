using Data.DTO.Order;
using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IOrderService
    {
        public Task<Order> GetSingleOrder(int orderId, int userId, PaginationParameters parameters);

        public Task<PaginationOrder> GetUserOrderList(int userId, PaginationParameters parameters);

        public Task<List<Order>> GetAllUsersOrders(PaginationParameters parameters);

        public Task<OrderRead> CreateOrder(CreateOrder createOrder, int userId);

        public Task<Order> PaidOrder(string orderNumber);

        public Task<Order> RemoveExpiredOrder(string orderNumber);
    }
}
