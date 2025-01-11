using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class ProductAutoMapper : Profile
{
    public ProductAutoMapper()
    {
        CreateMap<Product, ProductCreateDto>()
            .ForMember(x => x.CategoryIds, x => x.MapFrom(x => x.ProductCategories.Select(x => x.CategoryId)))
            .ReverseMap();

        CreateMap<Product, ProductUpdateDto>()
            .ForMember(x => x.MainImagePath, x => x.MapFrom(x => x.ProductImages.FirstOrDefault(x => x.IsMain) != null ? x.ProductImages.FirstOrDefault(x => x.IsMain)!.Path : string.Empty))
            .ForMember(x => x.ImagePaths, x => x.MapFrom(x => x.ProductImages.Where(x => !x.IsMain).Select(x => x.Path)))
            .ForMember(x => x.ImageIds, x => x.MapFrom(x => x.ProductImages.Where(x => !x.IsMain).Select(x => x.Id)))
            .ForMember(x => x.CategoryIds, x => x.MapFrom(x => x.ProductCategories.Select(x => x.CategoryId)))
            .ReverseMap()
            .ForMember(x => x.ProductSizes, x => x.Ignore())
            .ForMember(x => x.ProductCategories, x => x.Ignore());

        CreateMap<Product, ProductGetDto>()
                       .ForMember(x => x.Name, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Name : string.Empty))
                       .ForMember(x => x.Description, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Description : string.Empty))
                       .ForMember(x => x.MainImagePath, x => x.MapFrom(src => src.ProductImages.FirstOrDefault(img => img.IsMain) != null ? src.ProductImages.FirstOrDefault(img => img.IsMain)!.Path : string.Empty))
                       .ForMember(x => x.ImagePaths, x => x.MapFrom(src => src.ProductImages.Where(x => !x.IsMain).Select(x => x.Path)))
                       .ForMember(x => x.Categories, x => x.MapFrom(x => x.ProductCategories.Select(x => x.Category)));

        CreateMap<Product, ProductRelationDto>()
                       .ForMember(x => x.Name, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Name : string.Empty));


        CreateMap<Product, BestSellerProductGetDto>()
                           .ForMember(x => x.ParentCategory, x => x.MapFrom(x => x.ProductCategories.FirstOrDefault() != null ? x.ProductCategories.FirstOrDefault()!.Category.Parent : new()))
                           .ForMember(x => x.Category, x => x.MapFrom(x => x.ProductCategories.FirstOrDefault() != null ? x.ProductCategories.FirstOrDefault()!.Category : new()))
                           .ForMember(x => x.Name, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Name : string.Empty))
                           .ForMember(x => x.Description, x => x.MapFrom(src => src.ProductDetails.FirstOrDefault() != null ? src.ProductDetails.FirstOrDefault()!.Description : string.Empty))
                           .ForMember(x => x.MainImagePath, x => x.MapFrom(src => src.ProductImages.FirstOrDefault(x => x.IsMain) != null ? src.ProductImages.FirstOrDefault(x => x.IsMain)!.Path : string.Empty));

    }
}
