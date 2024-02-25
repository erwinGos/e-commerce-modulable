using Data.DTO.ProductDto;

namespace Data.DTO.Category
{
    public class CategoryRead
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<ProductRead> Products { get; set; } = new List<ProductRead>();
    }
}
