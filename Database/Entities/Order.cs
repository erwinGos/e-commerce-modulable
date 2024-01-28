using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Database.Entities
{
    public partial class Order
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public required string OrderNumber { get; set; }

        public int UserId { get; set; }

        public decimal Discount_amount { get; set; }

        public decimal Total_without_tax { get; set; } = 0;

        public decimal Total { get; set; } = 0;

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

        [Required]
        [StringLength(512)]
        public required string ParcelTracking { get; set;}

        public required DateTime EstimatedDeliveryDate { get; set; }

        public required DateTime DeliveryDate { get; set; }

        public bool HasBeenPaid { get; set; } = false;

        public required DateTime CreatedAt { get; set; }

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
