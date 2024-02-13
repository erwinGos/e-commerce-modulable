﻿using AutoMapper;
using Data.DTO.Cart;
using Data.DTO.Category;
using Data.DTO.Color;
using Data.DTO.Product;
using Data.DTO.Promo;
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

            //Color
            CreateMap<Color, ColorRead>();

            //Category
            CreateMap<Category, CategoryRead>();

            // Promo
            CreateMap<ProductPromo, Product>();
            CreateMap<CreatePromo, PromoCode>();

            // Product
            CreateMap<Product, CartProduct>();
            CreateMap<CreateProduct, Product>();
            CreateMap<UpdateProduct, Product>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Product, ProductRead>()
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Colors))
            .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.ProductImages));

            // ShoppingCart
            CreateMap<AddToCart, UserCart>();
            CreateMap<UserCart, CartRead>()
           .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }
    }
}
