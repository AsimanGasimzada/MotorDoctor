using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class SliderAutoMapper : Profile
{
    public SliderAutoMapper()
    {
        CreateMap<Slider, SliderCreateDto>().ReverseMap();
        CreateMap<Slider, SliderUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Slider, SliderGetDto>()
                        .ForMember(x => x.Title, x => x.MapFrom(x => x.SliderDetails.FirstOrDefault() != null ? x.SliderDetails.FirstOrDefault()!.Title : string.Empty))
                        .ForMember(x => x.Description, x => x.MapFrom(x => x.SliderDetails.FirstOrDefault() != null ? x.SliderDetails.FirstOrDefault()!.Description : string.Empty))
                        .ForMember(x => x.ButtonTitle, x => x.MapFrom(x => x.SliderDetails.FirstOrDefault() != null ? x.SliderDetails.FirstOrDefault()!.ButtonTitle : string.Empty));

    }
}
