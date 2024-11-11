using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class ProductDetailAutoMapper : Profile
{
    public ProductDetailAutoMapper()
    {
        CreateMap<ProductDetail, ProductDetailCreateDto>().ReverseMap();
        CreateMap<ProductDetail, ProductDetailUpdateDto>().ReverseMap();
    }
}
