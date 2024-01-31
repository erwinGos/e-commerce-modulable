using Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.DTO.Cart
{
    public class CartProduct
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int BrandId { get; set; }

        [Required]
        [StringLength(256)]
        public required string ProductName { get; set; }

        [StringLength(256)]
        public string Ean { get; set; } = "";

        public decimal Price { get; set; } = 0;

        public decimal PriceWithoutTax { get; set; } = 0;

        public string Description { get; set; } = "";

        public bool IsDeactivated { get; set; }

        public required virtual Brand Brand { get; set; }
    }
}
