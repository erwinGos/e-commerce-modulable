using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public partial class User
    {
        public int Id { get; set; }

        [StringLength(320)]
        [EmailAddress]
        public string Email { get; set; }

        [JsonIgnore]
        [StringLength(256)]
        public string Password { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string LastName { get; set; }

        public decimal Balance { get; set; } = 0;

        [JsonIgnore]
        public bool IsAdmin { get; set; }

        [JsonIgnore]
        public bool IsDeactivated { get; set; }

        [JsonIgnore]
        public bool IsBanned { get; set; }

        public DateTime RegistrationDate { get; set; }

        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

        public virtual ICollection<UserCart> ShoppingCart { get; set; } = new List<UserCart>();

        public virtual ICollection<PromoCode> PromoCodes { get; set; }
    }
}
