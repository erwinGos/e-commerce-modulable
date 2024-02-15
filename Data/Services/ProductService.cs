using AutoMapper;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
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

        public async Task<ProductRead> CreateProduct(CreateProduct createProduct)
        {
            try
            {
                Product product = await _productRepository.Insert(_mapper.Map<Product>(createProduct));
                return _mapper.Map<ProductRead>(product);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
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

        public async Task<ProductRead> UpdateProduct(Product updateProduct)
        {
            try
            {
                Product checkIfExists = await _productRepository.GetById(updateProduct.Id) ?? throw new Exception("Le produit n'existe pas."); ;
                Product updatedProduct = await _productRepository.Update(updateProduct);
                return _mapper.Map<ProductRead>(updatedProduct);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductRead> DeactivateProduct(int productId)
        {
            try
            {
                Product checkIfExists = await _productRepository.GetById(productId) ?? throw new Exception("Le produit n'existe pas."); ;
                checkIfExists.IsDeactivated = true;
                Product deletedProduct = await _productRepository.Update(checkIfExists);
                return _mapper.Map<ProductRead>(deletedProduct);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductRead> SaveUpDatabase(Product product)
        {
            try
            {

                return _mapper.Map<ProductRead>(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
