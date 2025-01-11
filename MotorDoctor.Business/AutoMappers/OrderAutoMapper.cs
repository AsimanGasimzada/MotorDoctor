using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class OrderAutoMapper : Profile
{
    public OrderAutoMapper()
    {
        CreateMap<Order, OrderCreateDto>().ReverseMap();
        CreateMap<Order, OrderUpdateDto>().ReverseMap();
        CreateMap<Order, OrderGetDto>().ReverseMap();
    }
}