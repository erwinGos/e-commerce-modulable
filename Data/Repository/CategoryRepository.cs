using Data.Repository.Contract;
using Database;
using Database.Entities;

namespace Data.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }

    }
}
