using Data.DTO.Order;
using Database.Entities;


namespace Data.Managers
{
    public class OrderManager
    {
        public static Order CreateOrderInstance(int userId, decimal totalWithoutTax, decimal total, decimal totalWeight, CreateOrder createOrder, Address address)
        {
            Order order = new()
            {
                UserId = userId,
                Total_without_tax = totalWithoutTax,
                Total = (total - createOrder.UsedBalance),
                TotalWeight = totalWeight,
                OrderNumber = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString(),
                Street = address.Street,
                PhoneNumber = address.PhoneNumber,
                City = address.City,
                PostalCode = address.PostalCode,
                Country = address.Country,
                ParcelTracking = "NONE",
                EstimatedDeliveryDate = DateTime.Now, // c'est le transporteur qui le retourne lors de la création de du ticket qui sera fait quand lacommande sera payé.
                DeliveryDate = DateTime.Now, // a mettre en non required parce que c'est le transporteur qui le retourne ça.
                CreatedAt = DateTime.Now
            };
            return order;
        }
    }
}
