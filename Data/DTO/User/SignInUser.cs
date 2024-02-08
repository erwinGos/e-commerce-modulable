using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.User
{
    public class SignInUser
    {
        [EmailAddress]
        [Required(ErrorMessage = "Le champs email est requis.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le champs mot de passe est requis.")]
        public string Password { get; set; }
    }
}
