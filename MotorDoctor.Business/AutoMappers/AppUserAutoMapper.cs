using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class AppUserAutoMapper : Profile
{
    public AppUserAutoMapper()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, UserGetDto>().ReverseMap();
    }
}
