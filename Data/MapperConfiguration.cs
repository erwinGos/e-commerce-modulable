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
            CreateMap<SignUpUser, User>();
            CreateMap<Product, CartProduct>();
            CreateMap<UserCart, CartRead>()
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));

        }
    }
}
