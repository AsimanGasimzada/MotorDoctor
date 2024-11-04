using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class BrandDetailAutoMapper : Profile
{
    public BrandDetailAutoMapper()
    {
        CreateMap<BrandDetail, BrandDetailCreateDto>().ReverseMap();
        CreateMap<BrandDetail, BrandDetailUpdateDto>().ReverseMap();
    }
}
