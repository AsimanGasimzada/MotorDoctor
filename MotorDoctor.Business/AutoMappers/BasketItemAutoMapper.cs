using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class BasketItemAutoMapper : Profile
{
    public BasketItemAutoMapper()
    {
        CreateMap<BasketItem, BasketItemCreateDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemGetDto>().ReverseMap();
    }
}