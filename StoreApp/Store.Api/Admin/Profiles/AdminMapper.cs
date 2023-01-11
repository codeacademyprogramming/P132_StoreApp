using AutoMapper;
using Store.Api.Admin.Dtos.CategoryDtos;
using Store.Api.Admin.Dtos.ProductDtos;
using Store.Api.Helpers;
using Store.Core.Entities;

namespace Store.Api.Admin.Profiles
{
    public class AdminMapper:Profile
    {
        private readonly IHttpContextAccessor _httpAccessor;

        public AdminMapper(IHttpContextAccessor httpAccessor)
        {
            _httpAccessor = httpAccessor;


            CreateMap<Category, CategoryGetDto>();
            CreateMap<CategoryPostDto, Category>();
            CreateMap<Category, CategoryListItemDto>();
            CreateMap<Category, CategoryInProductGetDto>();

            CreateMap<ProductPostDto, Product>();
            CreateMap<Product, ProductGetDto>()
                .ForMember(x => x.DiscountedPrice, f => f.MapFrom(s => s.SalePrice * (100 - s.DiscountPercent) / 100))
                .ForMember(x => x.ImageUrl, f => f.MapFrom(s =>  $"{_httpAccessor.HttpContext.Request.BaseUrl()}/uploads/products/{s.Image}"));

            CreateMap<Product, ProductListItemDto>();
        }
    }
}
