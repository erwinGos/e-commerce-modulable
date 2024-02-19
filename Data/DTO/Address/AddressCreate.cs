
using System.ComponentModel.DataAnnotations;

namespace Data.DTO.Address
{
    public class AddressCreate
    {
        public string Name { get; set; }

        public string Street { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
