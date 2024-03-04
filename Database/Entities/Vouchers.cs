using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public partial class Vouchers
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public required string Code { get; set; }

        public decimal Amount { get; set; }

        public bool HasBeenUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set;}

    }
}
