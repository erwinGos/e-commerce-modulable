using Data.DTO.Promo;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services.Contract
{
    public interface IPromoService
    {
        public Task<PromoCode> GetSinglePromo(int promoCodeId);

        public Task<List<PromoCode>> GetAll();

        public Task<PromoCode> CreatePromo(CreatePromo createPromo);

        public Task<PromoCode> Delete(int promoCodeId);

    }
}
