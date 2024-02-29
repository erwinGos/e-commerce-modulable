using AutoMapper;
using Data.DTO.Color;
using Data.DTO.Pagination;
using Data.DTO.ProductDto;
using Data.DTO.ProductOrder;
using Data.DTO.Promo;
using Data.Repository;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IPromoRepository _promoRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;

        public ProductService(IPromoRepository promoRepository, IColorRepository colorRepository, IProductRepository productRepository, IProductOrderRepository productOrderRepository, IBrandRepository brandRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _productOrderRepository = productOrderRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
            _promoRepository = promoRepository;
            _colorRepository = colorRepository;
        }

        public async Task<List<Product>> GetMostSoldProducts()
        {
            try
            {
                List<Product> products = [];
                List<CountProduct> countProduct = _productOrderRepository.GetMostSoldProduct();
                foreach (var product in countProduct)
                {
                    Product productFetched = await _productRepository.FindSingleBy(p => p.Id == product.ProductId, p => p.Brand);
                    products.Add(productFetched);
                }

                return products;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProductRead> CreateProduct(CreateProduct createProduct)
        {
            try
            {
                ICollection<Color> colorsList = [];
                ICollection<PromoCode> promoList = [];
                if (createProduct.Colors != null)
                {
                    foreach (ColorRelationnalAdd color in createProduct.Colors)
                    {
                        try
                        {
                            Color fetchColor = await _colorRepository.GetById(color.Id);
                            if (fetchColor != null)
                                colorsList.Add(fetchColor);
                        } catch(Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                if (createProduct.PromoCodes != null)
                {
                    foreach (PromoRelationnalAdd promo in createProduct.PromoCodes)
                    {
                        try
                        {
                            PromoCode fetchPromo = await _promoRepository.GetById(promo.Id);
                            if (fetchPromo != null)
                                promoList.Add(fetchPromo);
                        } catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                createProduct.Colors = null;
                createProduct.PromoCodes = null;
                Product productToCreate = _mapper.Map<Product>(createProduct);
                productToCreate.Colors = colorsList;
                productToCreate.PromoCodes = promoList;
                Product product = await _productRepository.Insert(productToCreate);
                return _mapper.Map<ProductRead>(product);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }
        }

        public async Task<PaginationProduct> GetProductListAsync(PaginationParameters parameters)
        {
            try
            {
                PaginationProduct FilteredProducts = _productRepository.GetProductListAsync(parameters);

                return FilteredProducts;
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

        public async Task<ProductRead> UpdateProduct(UpdateProduct updateProduct)
        {
            try
            {
                ICollection<Color> colorsList = [];
                ICollection<PromoCode> promoList = [];
                if (updateProduct.Colors != null)
                {
                    foreach (ColorRelationnalAdd color in updateProduct.Colors)
                    {
                        try
                        {
                            Color fetchColor = await _colorRepository.GetById(color.Id);
                            if (fetchColor != null)
                                colorsList.Add(fetchColor);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                if (updateProduct.PromoCodes != null)
                {
                    foreach (PromoRelationnalAdd promo in updateProduct.PromoCodes)
                    {
                        try
                        {
                            PromoCode fetchPromo = await _promoRepository.GetById(promo.Id);
                            if (fetchPromo != null)
                                promoList.Add(fetchPromo);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                    }
                }
                updateProduct.Colors = null;
                updateProduct.PromoCodes = null;
                Product productToUpdate = _mapper.Map<Product>(updateProduct);
                productToUpdate.Colors = colorsList;
                productToUpdate.PromoCodes = promoList;
                Product updatedProduct = await _productRepository.UpdateProduct(productToUpdate);
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
