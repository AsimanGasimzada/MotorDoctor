using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class WishlistItemAutoMapper : Profile
{
    public WishlistItemAutoMapper()
    {
        CreateMap<WishlistItem,WishlistItemCreateDto>().ReverseMap();
        CreateMap<WishlistItem,WishlistItemGetDto>().ReverseMap();
    }
}