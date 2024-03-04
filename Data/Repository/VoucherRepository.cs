using Data.DTO.Pagination;
using Data.Repository.Contract;
using Database;
using Database.Entities;

namespace Data.Repository
{
    public class VoucherRepository : GenericRepository<Vouchers>, IVoucherRepository
    {
        public VoucherRepository(DatabaseContext context): base(context)
        {
        }

        public async Task<List<Vouchers>> GetVoucherListPagination(PaginationParameters parameters)
        {
            try
            {
                parameters.MaxResults = Math.Min(parameters.MaxResults, 50);

                var skipAmount = (parameters.Page - 1) * parameters.MaxResults;

                var query = _table
                    .Skip(skipAmount)
                    .Take(parameters.MaxResults);

                var result = query.ToList();
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
