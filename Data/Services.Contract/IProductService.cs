using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IProductService
    {
        public Task<List<ProductRead>> GetProductListAsync(PaginationParameters parameters);

        public Task<ProductRead> FindOne(int productId);

        public Task<ProductRead> UpdateProduct(Product product);

        public Task<ProductRead> DeactivateProduct(int productId);

        public Task<ProductRead> CreateProduct(CreateProduct createProduct);

        public Task<List<Product>> ChangeBrandFromGroupedProduct(List<Product> products, int BrandId);

        public Task<List<Product>> GetAllProductsByBrand(int BrandId);
    }
}
