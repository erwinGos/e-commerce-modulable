

using Data.DTO.ProductDto;
using Database.Entities;

namespace Data.DTO.ProductOrderDto
{
    public class ProductOrderRead
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Discount_Amount { get; set; }

        public decimal TotalWithoutTax { get; set; }

        public decimal Total { get; set; }

        public decimal Reduction { get; set; }

        public string UsedPromoCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual ProductReadOrder? Product { get; set; }

        public virtual Return? Return { get; set; }
    }
}
