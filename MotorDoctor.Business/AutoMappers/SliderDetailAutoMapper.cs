using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class SliderDetailAutoMapper : Profile
{
    public SliderDetailAutoMapper()
    {
        CreateMap<SliderDetail, SliderDetailCreateDto>().ReverseMap();
        CreateMap<SliderDetail, SliderDetailUpdateDto>().ReverseMap();
    }
}