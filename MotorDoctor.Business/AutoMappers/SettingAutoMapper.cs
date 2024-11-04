using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class SettingAutoMapper : Profile
{
    public SettingAutoMapper()
    {
        CreateMap<Setting, SettingCreateDto>().ReverseMap();
        CreateMap<Setting, SettingUpdateDto>().ReverseMap().ForMember(x => x.Key, x => x.Ignore());
        CreateMap<Setting, SettingGetDto>()
            .ForMember(x => x.Value, x => x.MapFrom(x => x.SettingDetails.FirstOrDefault() != null ? x.SettingDetails.FirstOrDefault()!.Value : string.Empty));
    }
}
