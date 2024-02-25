using Data.DTO.Category;
using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface ICategoryService
    {
        public Task<List<CategoryRead>> GetAll(PaginationParameters parameters);

        public Task<Category> GetCategoryByName(string name);

        public Task<Category> GetCategoryById(int Id);

        public Task<Category> CreateCategory(CategoryCreate createCategory);

        public Task<CategoryRead> UpdateCategory(CategoryUpdate updateCategory);

        public Task<Category> DeleteCategory(int Id);
    }
}
