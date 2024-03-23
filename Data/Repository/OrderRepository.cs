using Data.DTO.Order;
using Data.DTO.Pagination;
using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRepository _productRepository;
        public OrderRepository(DatabaseContext db, IProductOrderRepository productOrderRepository, IProductRepository productRepository) : base(db)
        {
            _productOrderRepository = productOrderRepository;
            _productRepository = productRepository;
        }

        public async Task<PaginationOrder> GetOrderListAsync(int userId, PaginationParameters parameters)
        {
            try
            {
                if (parameters.MaxResults > 50)
                {
                    parameters.MaxResults = 50;
                }

                var queryMaxPage = _table
                    .Where(x => x.UserId == userId).ToArray();

                var TotalPages = Math.Ceiling((double)queryMaxPage.Length / parameters.MaxResults);

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

                var listOrder = await query.Where(o => o.UserId == userId).ToListAsync();
                return new PaginationOrder { Orders = listOrder , maxPages = (int)(TotalPages < 1 ? 1 : TotalPages) };
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

        public async Task<Order> ChangeToPaidOrder(string OrderNumber)
        {
            try
            {
                Order order = await this.FindSingleBy(order => order.OrderNumber == OrderNumber) ?? throw new Exception("La commande n'existe pas.");
                _db.Entry(order).State = EntityState.Detached;
                order.HasBeenPaid = true;
                var elementUpdated = _table.Update(order);
                await _db.SaveChangesAsync().ConfigureAwait(false);
                return elementUpdated.Entity;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> RemoveExpiredOrder(string OrderNumber)
        {
            try
            {
                Order order = await this.FindSingleBy(order => order.OrderNumber == OrderNumber, order => order.ProductOrders) ?? throw new Exception("La commande n'existe pas.");
                _db.Entry(order).State = EntityState.Detached;
                
                foreach(ProductOrder itemOrder in order.ProductOrders)
                {
                    Product product = await _productRepository.GetById(itemOrder.ProductId);
                    _db.Entry(product).State = EntityState.Detached;
                    product.CurrentStock += itemOrder.Quantity;

                    await _productRepository.Update(product);

                    await _productOrderRepository.Delete(itemOrder);
                }
                return await this.Delete(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
