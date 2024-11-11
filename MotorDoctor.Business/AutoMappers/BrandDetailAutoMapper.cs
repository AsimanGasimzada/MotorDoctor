using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class BrandDetailAutoMapper : Profile
{
    public BrandDetailAutoMapper()
    {
        CreateMap<BrandDetail, BrandDetailCreateDto>().ReverseMap();
        CreateMap<BrandDetail, BrandDetailUpdateDto>().ReverseMap();
    }
}
