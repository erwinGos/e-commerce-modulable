using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IVoucherService
    {
        public Task<List<Vouchers>> GetAllVouchers(PaginationParameters parameters);

        public Task<Vouchers> DeleteVoucher(int Id);

        public Task<Vouchers> UseVoucher(string code, int userId);

        public Task<Vouchers> CreateVoucher(Vouchers createVoucher);
    }
}
