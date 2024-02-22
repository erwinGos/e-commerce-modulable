using Data.DTO.ProductOrder;
using Database.Entities;

namespace Data.Repository.Contract
{
    public interface IProductOrderRepository : IGenericRepository<ProductOrder>
    {
        public List<CountProduct> GetMostSoldProduct();
    }
}
