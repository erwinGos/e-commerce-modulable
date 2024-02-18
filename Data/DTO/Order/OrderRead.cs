
using Data.DTO.ProductOrderDto;
using Database.Entities;

namespace Data.DTO.Order
{
    public class OrderRead
    {
        public int Id { get; set; }

        public required string OrderNumber { get; set; }

        public int UserId { get; set; }

        public decimal Discount_amount { get; set; }

        public decimal Total_without_tax { get; set; } = 0;

        public decimal Total { get; set; } = 0;

        public required string Street { get; set; }

        public required string PhoneNumber { get; set; }

        public required string City { get; set; }

        public required string PostalCode { get; set; }

        public required string Country { get; set; }

        public required string ParcelTracking { get; set; }

        public string StripePaymentUrl { get; set; }

        public required decimal TotalWeight { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public bool HasBeenPaid { get; set; } = false;

        public required DateTime CreatedAt { get; set; }

        public virtual ICollection<ProductOrderRead> ProductOrders { get; set; } = new List<ProductOrderRead>();
    }
}
