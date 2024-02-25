using System.ComponentModel.DataAnnotations;
using Data.DTO.ProductDto;

namespace Data.DTO.Category
{
    public class CategoryCreate
    {
        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        [StringLength(256)]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<ProductRelationnalAdd> Products { get; set; } = new List<ProductRelationnalAdd>();
    }
}
