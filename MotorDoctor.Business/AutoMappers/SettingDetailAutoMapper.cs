using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class SettingDetailAutoMapper : Profile
{
    public SettingDetailAutoMapper()
    {
        CreateMap<SettingDetail, SettingDetailCreateDto>().ReverseMap();
        CreateMap<SettingDetail, SettingDetailUpdateDto>().ReverseMap();
    }
}
