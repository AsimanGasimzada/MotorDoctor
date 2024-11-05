using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class BranchDetailAutoMapper : Profile
{
    public BranchDetailAutoMapper()
    {
        CreateMap<BranchDetail, BranchDetailCreateDto>().ReverseMap();
        CreateMap<BranchDetail, BranchDetailUpdateDto>().ReverseMap();
    }
}