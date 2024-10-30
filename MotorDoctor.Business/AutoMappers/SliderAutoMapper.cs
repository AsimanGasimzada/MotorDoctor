using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class SliderAutoMapper : Profile
{
    public SliderAutoMapper()
    {
        CreateMap<Slider, SliderCreateDto>().ReverseMap();
        CreateMap<Slider, SliderUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Slider, SliderGetDto>().ForMember(x => x.Title, x => x.MapFrom(x => x.SliderDetails.FirstOrDefault()!.Title))
                                           .ForMember(x => x.Description, x => x.MapFrom(x => x.SliderDetails.FirstOrDefault()!.Description))
                                           .ForMember(x => x.ButtonTitle, x => x.MapFrom(x => x.SliderDetails.FirstOrDefault()!.ButtonTitle));

    }
}
