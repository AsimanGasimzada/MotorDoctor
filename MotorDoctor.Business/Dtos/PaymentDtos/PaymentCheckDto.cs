using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Dtos;

public class PaymentCheckDto : IDto
{
    public string Token { get; set; } = null!;
    public int ID { get; set; }
    public PaymentStatuses STATUS { get; set; }
}