using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class CategoryAutoMapper : Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryCreateDto>().ReverseMap();
        CreateMap<Category, CategoryUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Category, CategoryGetDto>()
                            .ForMember(x => x.Name, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault() != null ? x.CategoryDetails.FirstOrDefault()!.Name : string.Empty))
                            .ForMember(x => x.Description, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault() != null ? x.CategoryDetails.FirstOrDefault()!.Description : string.Empty));

        CreateMap<Category, ParentCategoryDto>()
            .ForMember(x => x.Name, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault() != null ? x.CategoryDetails.FirstOrDefault()!.Name : string.Empty))
            .ForMember(x => x.ProductCount, x => x.MapFrom(x => x.Children.Sum(x => x.Products.Count)));

        CreateMap<Category, CategoryRelationDto>()
            .ForMember(x => x.Name, x => x.MapFrom(x => x.CategoryDetails.FirstOrDefault() != null ? x.CategoryDetails.FirstOrDefault()!.Name : string.Empty));
    }
}
