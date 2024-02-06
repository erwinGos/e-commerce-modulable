using Data.DTO.Pagination;
using Database.Entities;


namespace Data.Repository.Contract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<List<Order>> GetOrderListAsync(int userId, PaginationParameters parameters);

        public Task<List<Order>> GetAllOrdersPagination(PaginationParameters parameters);
    }
}
