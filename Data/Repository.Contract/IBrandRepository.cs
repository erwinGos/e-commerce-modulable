using Data.DTO.Pagination;
using Database.Entities;


namespace Data.Repository.Contract
{
    public interface IBrandRepository : IGenericRepository<Brand>
    {
        public Task<List<Brand>> GetBrandListAsync(PaginationParameters parameters);
    }
}
