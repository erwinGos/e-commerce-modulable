using Data.DTO.Category;
using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Repository.Contract
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public Task<List<Category>> GetCategoryListPagination(PaginationParameters parameters);
        public Task<Category> GetCategoryWithRelatedProducts(int Id);

        public Task<Category> UpdateCategory(Category updateCategory);
    }
}
