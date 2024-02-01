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
            List<Product> FilteredProducts = await _productRepository.GetProductListAsync(parameters);

            return _mapper.Map<List<ProductRead>>(FilteredProducts);
        }
    }
}
