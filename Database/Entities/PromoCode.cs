using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public partial class PromoCode
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public required string Code { get; set; }

        public bool HomeVisible { get; set; } = false;

        [StringLength(256)]
        public string Message { get; set; } = "";

        public decimal DiscountPercentage { get; set; }

        public bool SingleTimeUsage { get; set; } = true;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        public virtual ICollection<User> Users { get; set; }
    }
}
