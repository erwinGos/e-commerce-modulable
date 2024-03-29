﻿using Data.DTO.Pagination;
using Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Contract
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        public PaginationProduct GetProductListAsync(PaginationParameters parameters);

        public Task<Product> GetProductAsync(int productId);

        public Task<Product> UpdateProduct(Product product);
    }
}
