using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Cart
{
    public class AddToCart
    {
        public required int ProductId { get; set; }

        public int Quantity { get; set; } = 1; 
    }
}
