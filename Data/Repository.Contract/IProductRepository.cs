using Data.DTO.Pagination;
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
        public Task<List<Product>> GetProductListAsync(PaginationParameters parameters);

        public Task<Product> GetProductAsync(int productId);
    }
}
