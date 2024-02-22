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

        public async Task<List<Brand>> GetAllBrands()
        {
            try
            {
                return await _brandRepository.GetAll();
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
                Brand brandToCreate = _mapper.Map<Brand>(brandCreate);
                Brand createdBrand = await _brandRepository.Insert(brandToCreate);

                return createdBrand;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Brand> UpdateBrand(Brand brandCreate)
        {
            try
            {
                
                Brand updatedBrand = await _brandRepository.Update(brandCreate);
                return updatedBrand;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Brand> DeleteBrand(Brand brand, bool hasChild)
        {
            try
            {
                if(hasChild)
                {
                    throw new Exception("Vous ne pouvez pas supprimer cette marque car elle possède un ou plusieurs produits.");
                } else
                {
                    Brand brandToDelete = await _brandRepository.Delete(brand);
                    return brandToDelete;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
