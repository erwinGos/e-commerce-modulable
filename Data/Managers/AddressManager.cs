using Database.Entities;

namespace Data.Managers
{
    public class AddressManager
    {
        public static Address CreateAddressInstance(int userId)
        {
            Address address = new()
            {
                UserId = userId,
                Name = "Mon Adresse",
                Street = "74 rue francis de DJO",
                PhoneNumber = "0685995845",
                City = "Villeurbanne",
                PostalCode = "69100",
                Country = "France",
                CreatedAt = DateTime.Now
            };
            return address;
        }
    }
}
