using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public partial class Address
    {
        public int Id { get; set; }

        public required int UserId { get; set; }

        [StringLength(128)]
        public required string Name { get; set; }

        [Required]
        [StringLength(128)]
        public required string Street { get; set; }

        [StringLength(64)]
        public required string PhoneNumber { get; set; }

        [Required]
        [StringLength(128)]
        public required string City { get; set; }

        [Required]
        [StringLength(64)]
        public required string PostalCode { get; set; }

        [Required]
        [StringLength(128)]
        public required string Country { get; set; }

        public required DateTime CreatedAt { get; set; }
    }
}
