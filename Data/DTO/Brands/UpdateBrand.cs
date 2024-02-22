using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Brands
{
    public class UpdateBrand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nom est un champs requis.")]
        public required string Name { get; set; }

        public bool IsArchived { get; set; } = false;

        public IFormFile? Logo { get; set; }
    }
}
