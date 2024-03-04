using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Repository.Contract
{
    public interface IVoucherRepository : IGenericRepository<Vouchers>
    {
        public Task<List<Vouchers>> GetVoucherListPagination(PaginationParameters parameters);
    }
}
