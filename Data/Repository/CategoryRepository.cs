using Data.DTO.Category;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Data.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly IProductRepository _productRepository;

        public CategoryRepository(DatabaseContext context, IProductRepository productRepository) : base(context)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Category>> GetCategoryListPagination(PaginationParameters parameters)
        {
            try
            {
                parameters.MaxResults = Math.Min(parameters.MaxResults, 50);

                var skipAmount = (parameters.Page - 1) * parameters.MaxResults;

                var query = _table
                    .Skip(skipAmount)
                    .Take(parameters.MaxResults);

                var result = query.ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryWithRelatedProducts(int Id)
        {
            try
            {
                IQueryable<Category> query = _table;
                Category category = query
                    .Include(cat => cat.Products)
                    .Where(cat => cat.Id == Id).SingleOrDefault() ?? throw new Exception("La catégorie n'existe pas.");

                _db.Entry(category).State = EntityState.Detached;
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> UpdateCategory(Category updateCategory)
        {
            try
            {

                var categoryToUpdate = _table
                    .Include(c => c.Products)
                    .FirstOrDefault(c => c.Id == updateCategory.Id) ?? throw new Exception("Catégorie non trouvée.");

                if (categoryToUpdate != null)
                {
                    categoryToUpdate.Products.Clear();

                    categoryToUpdate.Name = updateCategory.Name ?? categoryToUpdate.Name;

                    categoryToUpdate.Products = updateCategory.Products;
                    await _db.SaveChangesAsync().ConfigureAwait(false);
                }
                return categoryToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
