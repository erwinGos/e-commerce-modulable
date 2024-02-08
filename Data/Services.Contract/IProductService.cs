using Data.DTO.Pagination;
using Data.DTO.Product;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IProductService
    {
        public Task<List<ProductRead>> GetProductListAsync(PaginationParameters parameters);

        public Task<ProductRead> FindOne(int productId);

        public Task<ProductRead> UpdateProduct(UpdateProduct product);

        public Task<ProductRead> DeactivateProduct(int productId);

        public Task<ProductRead> CreateProduct(CreateProduct createProduct);
    }
}
