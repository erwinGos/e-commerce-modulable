using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Voucher
{
    public class VoucherCreate
    {

        [Required(ErrorMessage = "Veuillez entrer une valeur. Ex : 50.00")]
        public decimal Amount { get; set; }

        public bool HasBeenUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Veuillez Choisir une date d'expiration.")]
        public DateTime ExpirationDate { get; set; }
    }
}
