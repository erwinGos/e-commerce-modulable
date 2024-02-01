using Data.DTO.Category;
using Data.DTO.Color;
using Database.Entities;

namespace Data.DTO.Product
{
    public class ProductRead
    {
        public int Id { get; set; }

        public required string ProductName { get; set; }

        public string Ean { get; set; } = "";

        public decimal Price { get; set; } = 0;

        public decimal PriceWithoutTax { get; set; } = 0;

        public string Description { get; set; } = "";

        public int CurrentStock { get; set; }

        public decimal Weight { get; set; }

        public bool IsDeactivated { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public required virtual Brand Brand { get; set; }

        public virtual ICollection<CategoryRead> Categories { get; set; } = new List<CategoryRead>();

        public virtual ICollection<ColorRead> Colors { get; set; } = new List<ColorRead>();

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
