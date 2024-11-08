using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class AttedanceDetailAutoMapper : Profile
{
    public AttedanceDetailAutoMapper()
    {
        CreateMap<AttendanceDetail, AttedanceDetailCreateDto>().ReverseMap();
        CreateMap<AttendanceDetail, AttedanceDetailUpdateDto>().ReverseMap();
    }
}
