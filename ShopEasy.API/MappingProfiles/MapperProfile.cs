using AutoMapper;
using ShopEasy.API.Dto;
using ShopEasy.API.Models;

namespace ShopEasy.API.MappingProfiles
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<Product, ProductCreateDto>();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductReadDto, Product>();

            CreateMap<AddToCartRequest, CartItem>();
            CreateMap<CartItem, AddToCartRequest>();
        }
    }
}
