

using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        public ProductImageService(IProductImageRepository productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public async Task<List<ProductImage>> CreateProductImages(List<ProductImage> productImage)
        {
            try
            {
                foreach(var image in productImage)
                {
                    await _productImageRepository.Update(image);
                }
                return productImage;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
