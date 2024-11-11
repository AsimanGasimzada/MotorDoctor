using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class ProductSizeAutoMapper : Profile
{
    public ProductSizeAutoMapper()
    {
        CreateMap<ProductSize, ProductSizeCreateDto>().ReverseMap();
        CreateMap<ProductSize, ProductSizeUpdateDto>().ReverseMap();
        CreateMap<ProductSize, ProductSizeGetDto>().ReverseMap();
        CreateMap<ProductSize, ProductSizeRelationDto>().ReverseMap();
    }
}
