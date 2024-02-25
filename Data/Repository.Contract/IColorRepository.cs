using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Repository.Contract
{
    public interface IColorRepository : IGenericRepository<Color>
    {
        public Task<List<Color>> GetColorListAsync(PaginationParameters parameters);
    }
}
