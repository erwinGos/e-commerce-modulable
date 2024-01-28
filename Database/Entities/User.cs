using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Database.Entities
{
    public partial class User
    {
        public int Id { get; set; }

        [StringLength(320)]
        public required string Email { get; set; }

        [JsonIgnore]
        [StringLength(256)]
        public required string Password { get; set; }

        [StringLength(256)]
        public required string Name { get; set; }

        [StringLength(256)]
        public required string LastName { get; set; }

        public decimal Balance { get; set; } = 0;

        [JsonIgnore]
        public Boolean IsAdmin { get; set; }

        [JsonIgnore]
        public Boolean IsDeactivated { get; set; }

        [JsonIgnore]
        public Boolean IsBanned { get; set; }

        public DateTime RegistrationDate { get; set; }


    }
}
