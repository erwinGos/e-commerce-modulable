using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data.DTO.User
{
    public class UpdateUser
    {
        [StringLength(320)]
        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(256)]
        public string? Password { get; set; }

        [StringLength(256)]
        public string OldPassword { get; set; }

        [StringLength(256)]
        public string? Name { get; set; }

        [StringLength(256)]
        public string? LastName { get; set; }
    }
}
