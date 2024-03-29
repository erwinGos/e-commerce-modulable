﻿using Data.DTO.Color;
using Data.DTO.Promo;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.DTO.ProductDto
{
    public class UpdateProduct
    {
        public int Id { get; set; }

        public int? BrandId { get; set; } = null;

        public string? ProductName { get; set; } = null;

        public string? Ean { get; set; } = null;

        public decimal? Price { get; set; } = null;

        public decimal? PriceWithoutTax { get; set; } = null;

        public string? Description { get; set; } = null;

        public string? StripeProductId { get; set; } = null;

        public int? CurrentStock { get; set; } = null;

        public decimal? Reduction { get; set; } = 0;

        public decimal? Weight { get; set; } = null;

        public bool? IsDeactivated { get; set; } = null;

        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ColorRelationnalAdd> Colors { get; set; }

        public ICollection<PromoRelationnalAdd> PromoCodes { get; set; }
    }
}
