
using Database.Entities;

namespace Data.DTO.Order
{
    public class PaginationOrder
    {
        public List<Database.Entities.Order> Orders { get; set; }

        public int maxPages { get; set; }
    }
}
