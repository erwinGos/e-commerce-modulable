
using Data.DTO.Product;

namespace Data.DTO.Order
{
    public class CreateOrder
    {
        public string PromoCode { get; set; } = null;

        public decimal UsedBalance { get; set; } = decimal.Zero;

        public required int AddressId { get; set; }

        public required List<ProductOrderCreate> Products { get; set; }
    }
}
