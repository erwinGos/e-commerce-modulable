using AutoMapper;
using Data.DTO.Pagination;
using Data.DTO.Product;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductRead>> GetProductListAsync(PaginationParameters parameters)
        {
            try
            {
                List<Product> FilteredProducts = await _productRepository.GetProductListAsync(parameters);

                return _mapper.Map<List<ProductRead>>(FilteredProducts);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductRead> FindOne(int productId)
        {
            try
            {
                Product product = await _productRepository.GetProductAsync(productId);

                return _mapper.Map<ProductRead>(product);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
