using AutoMapper;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class CategoryDetailAutoMapper : Profile
{
    public CategoryDetailAutoMapper()
    {
        CreateMap<CategoryDetail, CategoryDetailCreateDto>().ReverseMap();
        CreateMap<CategoryDetail, CategoryDetailUpdateDto>().ReverseMap();
    }
}
