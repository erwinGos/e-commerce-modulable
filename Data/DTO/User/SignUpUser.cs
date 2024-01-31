using System.ComponentModel.DataAnnotations;

namespace Data.DTO.User
{
    public class SignUpUser
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
