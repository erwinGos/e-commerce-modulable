using Data.DTO.Pagination;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetProductListAsync(PaginationParameters parameters)
        {
            List<Product> FilteredProducts = await _productRepository.GetProductListAsync(parameters);

            return FilteredProducts;
        }
    }
}
