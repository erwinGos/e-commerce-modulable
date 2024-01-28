using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Entities
{
    public partial class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public byte[]? Image { get; set; }
    }
}
