using AutoMapper;
using Data.DTO.Cart;
using Data.DTO.User;
using Database.Entities;

namespace Data
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            // Users
            CreateMap<SignUpUser, User>();

            // Product
            CreateMap<Product, CartProduct>();

            // ShoppingCart
            CreateMap<AddToCart, UserCart>();
            CreateMap<UserCart, CartRead>()
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }
    }
}
