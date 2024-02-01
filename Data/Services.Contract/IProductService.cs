using Data.DTO.Pagination;
using Data.DTO.Product;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IProductService
    {
        public Task<List<ProductRead>> GetProductListAsync(PaginationParameters parameters);
    }
}
