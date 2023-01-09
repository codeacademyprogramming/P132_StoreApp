using AutoMapper;
using Store.Api.Admin.Dtos.CategoryDtos;
using Store.Api.Admin.Dtos.ProductDtos;
using Store.Core.Entities;

namespace Store.Api.Admin.Profiles
{
    public class AdminMapper:Profile
    {
        public AdminMapper()
        {
            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Category, CategoryListItemDto>();
            CreateMap<Category, CategoryInProductGetDto>();

            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductGetDto>()
                .ForMember(x => x.DiscountedPrice, f => f.MapFrom(s => s.SalePrice * (100 - s.DiscountPercent) / 100));
        }
    }
}
