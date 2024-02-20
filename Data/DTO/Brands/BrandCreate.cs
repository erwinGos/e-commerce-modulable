

namespace Data.DTO.Brands
{
    public class BrandCreate
    {
        public required string Name { get; set; }

        public byte[]? Logo { get; set; } = new byte[256];

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
