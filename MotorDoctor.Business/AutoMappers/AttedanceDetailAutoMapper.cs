using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class AttedanceDetailAutoMapper : Profile
{
    public AttedanceDetailAutoMapper()
    {
        CreateMap<AttendanceDetail, AttedanceDetailCreateDto>().ReverseMap();
        CreateMap<AttendanceDetail, AttedanceDetailUpdateDto>().ReverseMap();
    }
}
