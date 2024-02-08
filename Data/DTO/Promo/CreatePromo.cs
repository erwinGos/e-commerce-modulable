using Data.DTO.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Promo
{
    public class CreatePromo
    {
        public required string Code { get; set; }

        public bool HomeVisible { get; set; } = false;

        public string Message { get; set; } = "";

        public decimal DiscountPercentage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public required DateTime ExpirationDate { get; set; }

        public virtual ICollection<CategoryPromo> Categories { get; set; }
    }
}
