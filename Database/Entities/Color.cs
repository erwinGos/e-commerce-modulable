using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
