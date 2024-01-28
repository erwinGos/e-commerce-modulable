using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public partial class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
