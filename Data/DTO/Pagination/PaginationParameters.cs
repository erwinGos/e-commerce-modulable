

namespace Data.DTO.Pagination
{
    public class PaginationParameters
    {
        public int Page { get; set; }

        public int MaxResults { get; set; }

        public string[] Colors { get; set; }

        public string[] Brands { get; set; }
    }
}
