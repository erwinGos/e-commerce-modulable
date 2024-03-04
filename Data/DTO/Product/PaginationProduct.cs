using Database.Entities;

namespace Data;

public class PaginationProduct
{
    public List<Product> Products { get; set;}

    public int maxPages { get; set; }
}
