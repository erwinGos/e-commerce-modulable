using Microsoft.AspNetCore.Http;

namespace Data.DTO.Brands
{
    public class BrandCreate
    {
        public required string Name { get; set; }

        public IFormFile? Logo { get; set; }
    }
}
