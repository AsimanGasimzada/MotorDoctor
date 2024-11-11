using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class OrderAutoMapper : Profile
{
    public OrderAutoMapper()
    {
        CreateMap<Order, OrderCreateDto>().ReverseMap().ForMember(x => x.TotalPrice, x => x.MapFrom(x => x.OrderItems.Sum(x => x.ProductSize.Price * x.Count)));
        CreateMap<Order, OrderUpdateDto>().ReverseMap();
        CreateMap<Order, OrderGetDto>().ReverseMap();
    }
}