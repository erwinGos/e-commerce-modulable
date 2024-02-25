using Data.DTO.Pagination;
using Data.Repository.Contract;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class ColorRepository : GenericRepository<Color>, IColorRepository
    {
        public ColorRepository(DatabaseContext context) : base(context)
        {
        }
        public async Task<List<Color>> GetColorListAsync(PaginationParameters parameters)
        {
            try
            {
                if (parameters.MaxResults > 50)
                {
                    parameters.MaxResults = 50;
                }
                var query = _table
                    .Skip((parameters.Page - 1) * parameters.MaxResults)
                    .Take(parameters.MaxResults);

                return query.ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
