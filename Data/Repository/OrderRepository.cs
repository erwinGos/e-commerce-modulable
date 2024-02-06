using Data.DTO.Pagination;
using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DatabaseContext db) : base(db)
        {
        }

        public async Task<List<Order>> GetOrderListAsync(int userId, PaginationParameters parameters)
        {
            try
            {
                if (parameters.MaxResults > 50)
                {
                    parameters.MaxResults = 50;
                }
                var query = _table
                    .Where(x => x.UserId == userId)
                    .Include(p => p.ProductOrders)
                        .ThenInclude(po => po.Product)
                            .ThenInclude(product => product.ProductImages)
                    .Include(p => p.ProductOrders)
                        .ThenInclude(po => po.Product)
                            .ThenInclude(product => product.Brand)
                    .Include(p => p.ProductOrders)
                        .ThenInclude(po => po.Return)
                    .Skip((parameters.Page - 1) * parameters.MaxResults)
                    .Take(parameters.MaxResults);

                return await query.Where(o => o.UserId == userId).ToListAsync();
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Order>> GetAllOrdersPagination(PaginationParameters parameters)
        {
            try
            {
                if (parameters.MaxResults > 50)
                {
                    parameters.MaxResults = 50;
                }
                var query = _table
                    .Skip((parameters.Page - 1) * parameters.MaxResults)
                    .Take(parameters.MaxResults);

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
