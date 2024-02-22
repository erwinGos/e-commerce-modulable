using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public partial class ProductOrder
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int OrderId { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;

        public decimal Discount_Amount { get; set; }

        public decimal TotalWithoutTax { get; set; }

        public decimal Total { get; set;}

        public decimal Reduction { get; set; }

        public string UsedPromoCode { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Return? Return { get; set; }
    }
}
