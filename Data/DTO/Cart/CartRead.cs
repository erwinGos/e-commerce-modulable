using System.Text.Json.Serialization;

namespace Data.DTO.Cart
{
    public class CartRead
    {
        public int Id { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 0;

        public string ColorName { get; set; }

        public virtual required CartProduct Product { get; set; }
    }
}
