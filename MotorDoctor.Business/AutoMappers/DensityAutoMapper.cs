using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;
internal class DensityAutoMapper : Profile
{
    public DensityAutoMapper()
    {
        CreateMap<Density, DensityGetDto>().ReverseMap();
        CreateMap<Density, DensityCreateDto>().ReverseMap();
        CreateMap<Density, DensityUpdateDto>().ReverseMap();
        CreateMap<Density, DensityRelationDto>().ReverseMap();
    }
}
