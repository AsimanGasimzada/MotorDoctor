namespace MotorDoctor.Business.Dtos;

public class PaymentCreateDto : IDto
{
    public decimal Amount { get; set; }
    public string Description { get; set; } = null!;
    public int OrderId { get; set; }
}
