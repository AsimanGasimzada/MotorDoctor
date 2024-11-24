using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class AboutDetailAutoMapper : Profile
{
    public AboutDetailAutoMapper()
    {
        CreateMap<AboutDetail, AboutDetailCreateDto>().ReverseMap();
        CreateMap<AboutDetail, AboutDetailUpdateDto>().ReverseMap();
    }
}
