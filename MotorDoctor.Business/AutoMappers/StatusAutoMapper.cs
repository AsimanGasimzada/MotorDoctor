using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class StatusAutoMapper:Profile
{
    public StatusAutoMapper()
    {
        CreateMap<Status, StatusGetDto>()
                .ForMember(x => x.Name, x => x.MapFrom(x => x.StatusDetails.FirstOrDefault() != null ? x.StatusDetails.FirstOrDefault()!.Name : string.Empty));
    }
}
