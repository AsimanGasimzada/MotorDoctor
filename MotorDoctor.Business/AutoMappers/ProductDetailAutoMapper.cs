using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class ProductDetailAutoMapper : Profile
{
    public ProductDetailAutoMapper()
    {
        CreateMap<ProductDetail, ProductDetailCreateDto>().ReverseMap();
        CreateMap<ProductDetail, ProductDetailUpdateDto>().ReverseMap();
    }
}
