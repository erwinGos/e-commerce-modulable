using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public partial class Return
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int OrderId { get; set; }

        [StringLength(256)]
        public string Address { get; set; } = "";

        public DateTime CreatedDate { get; set; }

        public bool IsRefunded { get; set; } = false;

        public bool IsReceived { get; set; } = false;

        public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
