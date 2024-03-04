using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Voucher
{
    public class VoucherRead
    {
        public int Id { get; set; }

        public required string Code { get; set; }

        public decimal Amount { get; set; }

        public bool HasBeenUsed { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
