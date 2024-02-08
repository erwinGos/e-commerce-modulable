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

        public int PromoCodeId { get; set; }

        public decimal TotalWithoutTax { get; set; } = decimal.Zero;

        public decimal Total { get; set; } = decimal.Zero;
    }
}
