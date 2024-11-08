using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class BrandAutoMapper : Profile
{
    public BrandAutoMapper()
    {
        CreateMap<Brand, BrandCreateDto>().ReverseMap();
        CreateMap<Brand, BrandUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Brand, BrandGetDto>()
                         .ForMember(x => x.Name, x => x.MapFrom(src => src.BrandDetails.FirstOrDefault() != null ? src.BrandDetails.FirstOrDefault()!.Name : string.Empty))
                         .ForMember(x => x.Description, x => x.MapFrom(src => src.BrandDetails.FirstOrDefault() != null ? src.BrandDetails.FirstOrDefault()!.Description : string.Empty));

        CreateMap<Brand, BrandRelationDto>()
                         .ForMember(x => x.Name, x => x.MapFrom(src => src.BrandDetails.FirstOrDefault() != null ? src.BrandDetails.FirstOrDefault()!.Name : string.Empty));


        CreateMap<Brand, BrandRelationDto>()
                        .ForMember(x => x.Name, x => x.MapFrom(src => src.BrandDetails.FirstOrDefault() != null ? src.BrandDetails.FirstOrDefault()!.Name : string.Empty));
    }
}
