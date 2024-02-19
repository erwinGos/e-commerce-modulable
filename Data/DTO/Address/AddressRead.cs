
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Address
{
    public class AddressRead
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
