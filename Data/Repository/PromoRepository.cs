using Data.Repository.Contract;
using Database.Entities;
using Database;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class PromoRepository : GenericRepository<PromoCode>, IPromoRepository
    {
        public PromoRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
