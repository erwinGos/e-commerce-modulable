using System.ComponentModel.DataAnnotations;

namespace Data.DTO.ProductDto
{
    public class CreateProduct
    {
        [Required(ErrorMessage ="Le choix d'une marque est requis.")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Le nom nom du produit est requis.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un numero / code produit.")]
        public string Ean { get; set; }

        [Required(ErrorMessage = "Veuillez rentrer un prix TTC.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Veuillez rentrer un prix HT.")]
        public decimal PriceWithoutTax { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Veuillez Definir un stock initial.")]
        public int CurrentStock { get; set; }

        [Required(ErrorMessage = "Veuillez saisir un poids (en grammes).")]
        public decimal Weight { get; set; }

        public string StripeProductId { get; set; } = "";

        public bool IsDeactivated { get; set; } = false;

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
