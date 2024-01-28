using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Database.Entities
{
    public partial class UserCart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [JsonIgnore]
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 0;

        public virtual required Product Product { get; set; }
    }
}
