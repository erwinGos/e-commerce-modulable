using AutoMapper;
using Data.DTO.Category;
using Data.DTO.ProductDto;
using Data.DTO.Pagination;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;
using Data.DTO.Promo;




namespace Data.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        private readonly IProductRepository _productRepository;

        private readonly IPromoRepository _promoRepository;

        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IProductRepository productRepository, IPromoRepository promoRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _promoRepository = promoRepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryRead>> GetAll(PaginationParameters parameters)
        {
            try
            {
                List<Category> categories = await _categoryRepository.GetCategoryListPagination(parameters);
                return _mapper.Map<List<CategoryRead>>(categories);
            } catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            try
            {
                return await _categoryRepository.FindSingleBy(cat => cat.Name == name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            try
            {
                return await _categoryRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> CreateCategory(CategoryCreate createCategory)
        {
            try
            {
                Category categoryToCreate = _mapper.Map<Category>(createCategory);
                List<Product> productList = [];
                foreach(ProductRelationnalAdd product in createCategory.Products)
                {
                    Product productFetched = await _productRepository.GetById(product.Id);
                    if(product != null)
                    {
                        productList.Add(productFetched);
                    }
                }
                categoryToCreate.Products = productList;
                return await _categoryRepository.Insert(categoryToCreate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryRead> UpdateCategory(CategoryUpdate updateCategory)
        {
            try
            {
                List<Product> productList = [];
                Category category = await _categoryRepository.GetCategoryWithRelatedProducts(updateCategory.Id) ?? throw new Exception("La catégorie que vous avez indiqué n'existe pas.");
                foreach (ProductRelationnalAdd product in updateCategory.Products)
                {
                    Product productFetched = await _productRepository.GetById(product.Id);
                    if (productFetched != null)
                    {
                        productList.Add(productFetched);
                    }
                }
                Category categoryToUpdate = _mapper.Map<Category>(updateCategory);
                categoryToUpdate.Products = productList;
                Category updatedCategory = await _categoryRepository.UpdateCategory(categoryToUpdate);
                return _mapper.Map<CategoryRead>(updatedCategory);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> DeleteCategory(int Id)
        {
            try
            {
                Category categoryToDelete = await _categoryRepository.GetById(Id) ?? throw new Exception("Cette catégorie n'existe pas, ou a déjà été supprimée.");
                return await _categoryRepository.Delete(categoryToDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
