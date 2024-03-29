﻿using Data.DTO.Order;
using Data.DTO.Pagination;
using Database.Entities;


namespace Data.Repository.Contract
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<PaginationOrder> GetOrderListAsync(int userId, PaginationParameters parameters);

        public Task<List<Order>> GetAllOrdersPagination(PaginationParameters parameters);

        public Task<Order> ChangeToPaidOrder(string OrderNumber);

        public Task<Order> RemoveExpiredOrder(string OrderNumber);
    }
}
