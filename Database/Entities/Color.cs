using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public partial class Color
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public required string Name { get; set; }

        [Required]
        [StringLength(6)]
        public required string Hex { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
