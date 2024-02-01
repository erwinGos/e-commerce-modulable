using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Color
{
    public class ColorRead
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Hex { get; set; }
    }
}
