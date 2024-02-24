

namespace Data.DTO.ProductDto
{
    public class ProductReadOrder
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
    }
}
