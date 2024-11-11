using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class AttedanceAutoMapper : Profile
{
    public AttedanceAutoMapper()
    {
        CreateMap<Attendance, AttendanceCreateDto>().ReverseMap();
        CreateMap<Attendance, AttendanceUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Attendance, AttendanceGetDto>()
                            .ForMember(x => x.Name, x => x.MapFrom(x => x.AttendanceDetails.FirstOrDefault() != null ? x.AttendanceDetails.FirstOrDefault()!.Name : string.Empty))
                            .ForMember(x => x.Description, x => x.MapFrom(x => x.AttendanceDetails.FirstOrDefault() != null ? x.AttendanceDetails.FirstOrDefault()!.Description : string.Empty));
    }
}
