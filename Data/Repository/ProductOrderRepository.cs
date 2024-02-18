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

    }
}
