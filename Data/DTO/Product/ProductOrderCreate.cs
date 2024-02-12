using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.DTO.Product
{
    public class ProductOrderCreate
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
