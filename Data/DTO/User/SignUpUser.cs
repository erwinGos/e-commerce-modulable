using System.ComponentModel.DataAnnotations;

namespace Data.DTO.User
{
    public class SignUpUser
    {
        [EmailAddress]
        [Required(ErrorMessage = "Le champs email est requis.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champs mot de passe est requis.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le champs Prénom est requis.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Le champs Nom est requis.")]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}
