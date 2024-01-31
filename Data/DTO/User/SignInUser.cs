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
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
