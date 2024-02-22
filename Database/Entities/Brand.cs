using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public partial class Brand
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        public required string Name { get; set; }

        public bool IsArchived { get; set; } = false;

        public byte[]? Logo { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
