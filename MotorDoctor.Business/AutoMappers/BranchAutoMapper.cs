using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class BranchAutoMapper : Profile
{
    public BranchAutoMapper()
    {
        CreateMap<Branch, BranchCreateDto>().ReverseMap();
        CreateMap<Branch, BranchUpdateDto>().ReverseMap().ForMember(x => x.ImagePath, x => x.Ignore());
        CreateMap<Branch, BranchGetDto>()
                              .ForMember(x => x.Name, x => x.MapFrom(src => src.BranchDetails.FirstOrDefault() != null ? src.BranchDetails.FirstOrDefault()!.Name : string.Empty))
                              .ForMember(x => x.Location, x => x.MapFrom(src => src.BranchDetails.FirstOrDefault() != null ? src.BranchDetails.FirstOrDefault()!.Location : string.Empty))
                              .ForMember(x => x.WorkHours, x => x.MapFrom(src => src.BranchDetails.FirstOrDefault() != null ? src.BranchDetails.FirstOrDefault()!.WorkHours : string.Empty))
                              .ForMember(x => x.PhoneNumber, x => x.MapFrom(src => src.BranchDetails.FirstOrDefault() != null ? src.BranchDetails.FirstOrDefault()!.PhoneNumber : string.Empty));
    }
}
