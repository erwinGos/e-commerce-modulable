﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Address
{
    public class AddressUpdate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }
}
