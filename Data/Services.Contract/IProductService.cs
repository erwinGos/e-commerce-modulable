using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IProductService
    {
        public Task<List<Product>> GetProductListAsync(PaginationParameters parameters);
    }
}
