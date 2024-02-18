using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        
        [JsonIgnore]
        public int BrandId { get; set; }

        [Required]
        [StringLength(256)]
        public required string ProductName { get; set; }

        [StringLength(256)]
        public string Ean { get; set; } = "";

        public decimal Price { get; set; } = 0;

        public decimal PriceWithoutTax { get; set; } = 0;

        [StringLength(256)]
        public string Description { get; set; } = "";

        public int CurrentStock { get; set; }

        public decimal Weight { get; set; }

        public bool IsDeactivated { get; set; }

        [StringLength(256)]
        public string StripeProductId { get; set; } = "";

        public string StripePriceId { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set;}

        public virtual Brand Brand { get; set; }

        public virtual ICollection<PromoCode> PromoCodes { get; set; } = new List<PromoCode>();

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Color> Colors { get; set; } = new List<Color>();

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    }
}
