using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class AboutAutoMapper : Profile
{
    public AboutAutoMapper()
    {
        CreateMap<About, AboutCreateDto>().ReverseMap();
        CreateMap<About, AboutUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());

        CreateMap<About, AboutGetDto>().ForMember(x => x.Name, x => x.MapFrom(x => x.AboutDetails.FirstOrDefault()!.Name))
                                         .ForMember(x => x.Description, x => x.MapFrom(x => x.AboutDetails.FirstOrDefault()!.Description));

    }
}
