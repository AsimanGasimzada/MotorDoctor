using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.AutoMappers;

internal class PaymentAutoMapper : Profile
{
    public PaymentAutoMapper()
    {
        CreateMap<Payment, PaymentGetDto>().ReverseMap();
    }
}
