using AutoMapper;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

public class SubscriberAutoMapper:Profile
{
    public SubscriberAutoMapper()
    {
        CreateMap<Subscriber,SubscriberCreateDto>().ReverseMap();
        CreateMap<Subscriber,SubscriberUpdateDto>().ReverseMap();
        CreateMap<Subscriber,SubscriberGetDto>().ReverseMap();
    }
}
