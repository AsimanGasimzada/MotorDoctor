namespace MotorDoctor.Business.Dtos;
public class PaymentResponseDto : IDto
{
    public OrderDto Order { get; set; } = null!;
    public int Id { get; set; }
}

public class OrderDto : IDto
{
    public int Id { get; set; }
    public string Password { get; set; } = null!;
    public string Status { get; set; } = null!;
    public string HppUrl { get; set; } = null!;
    public string Cvv2AuthStatus { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
