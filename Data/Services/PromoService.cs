using AutoMapper;
using Data.DTO.Promo;
using Data.Repository.Contract;
using Data.Services.Contract;
using Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class PromoService : IPromoService
    {
        private readonly IPromoRepository _promoRepository;

        private readonly ICategoryRepository _categoryRepository;

        private readonly IMapper _mapper;

        public PromoService(IPromoRepository promoRepository,ICategoryRepository categoryRepository,IMapper mapper)
        {
            _promoRepository = promoRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PromoCode> GetSinglePromo(int promoCodeId)
        {
            try
            {
                PromoCode promo = await _promoRepository.GetById(promoCodeId);
                return promo;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<PromoCode>> GetAll()
        {
            try
            {
                List<PromoCode> promo = await _promoRepository.GetAll();
                return promo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PromoCode> CreatePromo(CreatePromo createPromo)
        {
            try
            {
                PromoCode transformedPromo = _mapper.Map<PromoCode>(createPromo);
                var categoriesToAttach = new List<Category>();
                foreach (var category in transformedPromo.Categories.ToList())
                {
                    var existingCategory = await _categoryRepository.GetById(category.Id);
                    if (existingCategory != null)
                    {
                        categoriesToAttach.Add(existingCategory);
                    }
                }

                transformedPromo.Categories = categoriesToAttach;
                PromoCode promo = await _promoRepository.Insert(transformedPromo);
                return promo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<PromoCode> Delete(int promoCodeId)
        {
            try
            {
                PromoCode promoToDelete = await _promoRepository.GetById(promoCodeId) ?? throw new Exception("Ce code promo n'existe pas, ou a déjà été supprimé.");
                return await _promoRepository.Delete(promoToDelete);
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
