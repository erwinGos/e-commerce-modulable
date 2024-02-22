using Data.DTO.ProductOrder;
using Data.Repository.Contract;
using Database;
using Database.Entities;

namespace Data.Repository
{
    public class ProductOrderRepository : GenericRepository<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(DatabaseContext context) : base(context)
        {
        }


        public List<CountProduct> GetMostSoldProduct()
        {
            try
            {
                var mostSoldProduct = _table
                .GroupBy(po => po.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    Count = group.Sum(po => po.Quantity)
                })
                .OrderByDescending(x => x.Count)
                .Take(3);

                List<CountProduct> products = new List<CountProduct>();
                foreach (var product in mostSoldProduct)
                {
                    products.Add(new CountProduct { ProductId = product.ProductId, Count = product.Count });
                }

                return products;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
