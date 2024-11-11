using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class CategoryDetailAutoMapper : Profile
{
    public CategoryDetailAutoMapper()
    {
        CreateMap<CategoryDetail, CategoryDetailCreateDto>().ReverseMap();
        CreateMap<CategoryDetail, CategoryDetailUpdateDto>().ReverseMap();
    }
}
