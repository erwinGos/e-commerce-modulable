using Data.DTO.Order;
using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IOrderService
    {
        public Task<Order> GetSingleOrder(int orderId, int userId, PaginationParameters parameters);

        public Task<List<Order>> GetUserOrderList(int userId, PaginationParameters parameters);

        public Task<List<Order>> GetAllUsersOrders(PaginationParameters parameters);
    }
}
