using MotorDoctor.Core.Enum;

namespace MotorDoctor.Business.Dtos;

public class PaymentGetDto : IDto
{
    public int OrderId { get; set; }
    public int ReceptId { get; set; }
    public string SecretId { get; set; } = null!;
    public PaymentStatuses PaymentStatus { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
