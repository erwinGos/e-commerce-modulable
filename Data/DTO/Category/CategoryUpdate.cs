using Data.DTO.ProductDto;
using Data.DTO.Promo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Category
{
    public class CategoryUpdate
    {
        [Required(ErrorMessage = "Vous devez choisir une category.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
        [StringLength(256)]
        public required string Name { get; set; }

        public virtual ICollection<ProductRelationnalAdd> Products { get; set; } = new List<ProductRelationnalAdd>();
    }
}
