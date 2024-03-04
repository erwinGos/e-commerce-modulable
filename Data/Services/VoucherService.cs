using Data.DTO.Pagination;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IUserRepository _userRepository;
        public VoucherService(IVoucherRepository voucherRepository, IUserRepository userRepository)
        {
            _voucherRepository = voucherRepository;
            _userRepository = userRepository;
        }

        public async Task<Vouchers> DeleteVoucher(int Id)
        {
            try
            {
                Vouchers voucher = await _voucherRepository.FindSingleBy(voucher => voucher.Id == Id) ?? throw new Exception("Ce code n'existe pas ou a déjà été supprimé.");
                return await _voucherRepository.Delete(voucher);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Vouchers>> GetAllVouchers(PaginationParameters parameters)
        {
            try
            {
                return await _voucherRepository.GetVoucherListPagination(parameters);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vouchers> CreateVoucher(Vouchers createVoucher)
        {
            try
            {
                return await _voucherRepository.Insert(createVoucher);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Vouchers> UseVoucher(string code, int userId)
        {
            try
            {
                User user = await _userRepository.GetById(userId) ?? throw new Exception("Nous ne pouvons pas ajouter ce code vers l'utilisateur souhaité.");
                Vouchers voucher = await _voucherRepository.FindSingleBy(voucher => voucher.Code == code && voucher.HasBeenUsed == false) ?? throw new Exception("Ce code est invalide ou a déjà été utilisé.");

                user.Balance += voucher.Amount;
                voucher.HasBeenUsed = true;
                await _userRepository.Update(user);
                await _voucherRepository.Update(voucher);
                return voucher;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
