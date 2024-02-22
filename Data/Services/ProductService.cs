using AutoMapper;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Data.Repository;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
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

        public async Task<List<Product>> GetAllProductsByBrand(int brandId)
        {
            try
            {
                List<Product> FilteredProducts = await _productRepository.FindBy(product => product.BrandId == brandId);

                return FilteredProducts;
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
                Product updatedProduct = await _productRepository.Update(updateProduct, updateProduct.Id);
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

        public async Task<List<Product>> ChangeBrandFromGroupedProduct(List<Product> products, int BrandId)
        {
            try
            {
                Brand brand = await _brandRepository.GetById(BrandId) ?? throw new Exception("Cette marque n'existe pas veuillez verifier votre entrée.");
                List<Product> result = new List<Product>();
                foreach (Product product in products)
                {
                    product.BrandId = brand.Id;
                    Product changedProduct = await _productRepository.Update(product);
                    result.Add(changedProduct);
                }
                return result;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
