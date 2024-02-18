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

        private readonly IProductRepository _productRepository;

        private readonly IMapper _mapper;

        public PromoService(IPromoRepository promoRepository, IProductRepository productRepository, IMapper mapper)
        {
            _promoRepository = promoRepository;
            _productRepository = productRepository;
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
                var productsToAttach = new List<Product>();
                foreach (var product in transformedPromo.Products.ToList())
                {
                    var existingProduct = await _productRepository.GetById(product.Id);
                    if (existingProduct != null)
                    {
                        productsToAttach.Add(existingProduct);
                    }
                }

                transformedPromo.Products = productsToAttach;
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
