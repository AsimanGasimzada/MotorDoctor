using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class OrderItemAutoMapper : Profile
{
    public OrderItemAutoMapper()
    {
        CreateMap<OrderItem, OrderItemCreateDto>().ReverseMap().ForMember(x => x.ProductSize, x => x.Ignore()).ForMember(x => x.StaticPrice, x => x.MapFrom(x => x.ProductSize.Price));
        CreateMap<OrderItem, OrderItemUpdateDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemGetDto>().ReverseMap();
        CreateMap<OrderItemCreateDto, BasketItemGetDto>().ReverseMap();
        CreateMap<OrderItemGetDto, BasketItemGetDto>().ReverseMap().ForMember(x => x.StaticPrice, x => x.MapFrom(x => x.ProductSize.Price));
    }
}
