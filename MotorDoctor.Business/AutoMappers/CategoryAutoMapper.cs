using AutoMapper;
using MotorDoctor.Business.Dtos;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class CategoryAutoMapper : Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Category, CategoryGetDto>()
                            .ForMember(x => x.Name, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault()!.Name))
                            .ForMember(x => x.Description, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault()!.Description));
    }
}
