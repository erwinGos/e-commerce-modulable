using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public partial class WebsiteSettings
    {
        [Required]
        [StringLength(256)]
        public required string WebsiteName { get; set; }

        public required byte[] MainLogo { get; set; }

        public int MainProductId { get; set; }

        [StringLength(256)]
        public string Address { get; set; } = "";

        public decimal DefaultTax { get; set; }

        [StringLength(256)]
        public string StripeApi { get; set; } = "";

        public DateTime LastUpdateAt { get; set; }
    }
}
