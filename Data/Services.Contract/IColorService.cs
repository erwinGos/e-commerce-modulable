using Data.DTO.Color;
using Data.DTO.Pagination;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IColorService
    {
        public Task<List<Color>> GetColorListAsync(PaginationParameters parameters);

        public Task<Color> GetSingleByName(string Name);

        public Task<Color> GetSingleById(int Id);

        public Task<Color> CreateColor(Color createColor);

        public Task<Color> UpdateColor(Color updateColor);

        public Task<Color> DeleteColor(Color color);
    }
}
