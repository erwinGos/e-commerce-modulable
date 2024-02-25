using Data.DTO.Pagination;
using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Data.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        protected readonly DatabaseContext _db;

        protected readonly DbSet<Product> _table;

        public ProductRepository(DatabaseContext context) : base(context)
        {
            _db = context;
            _table = _db.Set<Product>();
        }

        public async Task<List<Product>> GetProductListAsync(PaginationParameters parameters)
        {
            try
            {
                if(parameters.MaxResults > 50)
                {
                    parameters.MaxResults = 50;
                }
                var query = _table
                    .Include(p => p.Brand)
                    .Include(p => p.Colors)
                    .Include(p => p.Categories)
                    .Include(p => p.ProductImages)
                    .Skip((parameters.Page - 1) * parameters.MaxResults)
                    .Take(parameters.MaxResults);

                var filteredByBrand = query.Where(product => parameters.Brands.Length > 0 ? parameters.Brands.Contains(product.Brand.Name) : true);
                var filteredByColors = filteredByBrand.Where(product => parameters.Colors.Length > 0 ? product.Colors.Any(color => parameters.Colors.Contains(color.Name)) : true).ToList();
                return filteredByColors;

            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> GetProductAsync(int productId)
        {
            try
            {
                var query = _table
                    .Include(p => p.Brand)
                    .Include(p => p.ProductImages)
                    .Include(p => p.Categories)
                    .Include(p => p.Colors);
                Product product = query.SingleOrDefault(product => product.Id == productId);
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Product> UpdateProduct(Product updateProduct)
        {
            try
            {

                var productToUpdate = _table
                    .Include(c => c.Colors)
                    .Include(c => c.PromoCodes)
                    .FirstOrDefault(c => c.Id == updateProduct.Id) ?? throw new Exception("Produit non trouvée.");

                if (productToUpdate != null)
                {
                    productToUpdate.Colors.Clear();
                    productToUpdate.PromoCodes.Clear();

                    productToUpdate.BrandId = updateProduct.BrandId;
                    productToUpdate.ProductName = updateProduct.ProductName;
                    productToUpdate.Ean = updateProduct.Ean;
                    productToUpdate.Price = updateProduct.Price;
                    productToUpdate.PriceWithoutTax = updateProduct.PriceWithoutTax;
                    productToUpdate.Description = updateProduct.Description;
                    productToUpdate.CurrentStock = updateProduct.CurrentStock;
                    productToUpdate.Weight = updateProduct.Weight;
                    productToUpdate.IsDeactivated = updateProduct.IsDeactivated;
                    productToUpdate.Reduction = updateProduct.Reduction;
                    productToUpdate.LastUpdatedAt = updateProduct.LastUpdatedAt;
                    productToUpdate.PromoCodes = updateProduct.PromoCodes;
                    productToUpdate.Colors = updateProduct.Colors;

                    await _db.SaveChangesAsync().ConfigureAwait(false);
                }
                return productToUpdate;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
