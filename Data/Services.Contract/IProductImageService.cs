

using Database.Entities;

namespace Data.Services.Contract
{
    public interface IProductImageService
    {
        public Task<List<ProductImage>> CreateProductImages(List<ProductImage> productImage);
    }
}
