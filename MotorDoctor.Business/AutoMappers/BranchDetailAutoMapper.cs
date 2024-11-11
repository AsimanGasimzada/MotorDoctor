using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class BranchDetailAutoMapper : Profile
{
    public BranchDetailAutoMapper()
    {
        CreateMap<BranchDetail, BranchDetailCreateDto>().ReverseMap();
        CreateMap<BranchDetail, BranchDetailUpdateDto>().ReverseMap();
    }
}