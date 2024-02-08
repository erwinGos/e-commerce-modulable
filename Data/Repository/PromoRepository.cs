using Data.Repository.Contract;
using Database.Entities;
using Database;

namespace Data.Repository
{
    public class PromoRepository : GenericRepository<PromoCode>, IPromoRepository
    {
        public PromoRepository(DatabaseContext context) : base(context)
        {
        }

    }
}
