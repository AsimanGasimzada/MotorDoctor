using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class AdvertisementAutoMapper : Profile
{
    public AdvertisementAutoMapper()
    {
        CreateMap<Advertisement, AdvertisementCreateDto>().ReverseMap();
        CreateMap<Advertisement, AdvertisementUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Advertisement, AdvertisementGetDto>().ReverseMap();
    }
}
