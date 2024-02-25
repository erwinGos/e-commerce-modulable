using Data.DTO.Pagination;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;


namespace Data.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public async Task<List<Color>> GetColorListAsync(PaginationParameters parameters)
        {
            try
            {
                return await _colorRepository.GetColorListAsync(parameters);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Color> GetSingleByName(string Name)
        {
            try
            {
                return await _colorRepository.FindSingleBy(color => color.Name == Name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Color> GetSingleById(int Id)
        {
            try
            {
                return await _colorRepository.GetById(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Color> CreateColor(Color createColor)
        {
            try
            {
                return await _colorRepository.Insert(createColor);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Color> UpdateColor(Color updateColor)
        {
            try
            {
                return await _colorRepository.Update(updateColor);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Color> DeleteColor(Color color)
        {
            try
            {
                return await _colorRepository.Delete(color);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
