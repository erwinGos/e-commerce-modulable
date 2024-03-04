using Data.DTO.Pagination;
using Data.Repository.Contract;
using Database;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BrandRepository : GenericRepository<Brand> , IBrandRepository
    {
        public BrandRepository(DatabaseContext context) : base(context)
        {
        }
        public async Task<List<Brand>> GetBrandListAsync(PaginationParameters parameters)
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
