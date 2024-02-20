using AutoMapper;
using Data.DTO.Brands;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;

namespace Data.Services
{
    public class BrandService : IBrandService
    {

        private readonly IBrandRepository _brandRepository;

        private readonly IMapper _mapper;

        public BrandService(IMapper mapper, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public async Task<Brand> GetSingleBrandById(int Id)
        {
            try
            {
                return await _brandRepository.GetById(Id);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Brand> GetSingleBrandByName(string Name)
        {
            try
            {
                return await _brandRepository.FindSingleBy(brand => brand.Name == Name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Brand> CreateBrand(Brand brandCreate)
        {
            try
            {
                Brand createdBrand = await _brandRepository.Insert(brandCreate);

                return createdBrand;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
