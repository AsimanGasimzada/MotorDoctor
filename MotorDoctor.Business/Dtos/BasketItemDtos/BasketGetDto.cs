namespace MotorDoctor.Business.Dtos;

public class BasketGetDto : IDto
{
    public int Count { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public decimal DiscountedTotal { get; set; }
    public List<BasketItemGetDto> Items { get; set; } = [];
}