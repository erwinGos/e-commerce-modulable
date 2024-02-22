using Data.DTO.Brands;
using Database.Entities;

namespace Data.Services.Contract
{
    public interface IBrandService
    {
        public Task<Brand> GetSingleBrandById(int Id);

        public Task<Brand> GetSingleBrandByName(string Name);

        public Task<Brand> CreateBrand(Brand createBrand);

        public Task<Brand> DeleteBrand(Brand createBrand, bool hasChild);
    }
}
