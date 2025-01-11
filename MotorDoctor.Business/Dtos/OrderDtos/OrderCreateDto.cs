namespace MotorDoctor.Business.Dtos;

public class OrderCreateDto : IDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<OrderItemCreateDto>? OrderItems { get; set; } = [];
    public decimal Total { get; set; }
    public decimal DiscountedTotal { get; set; }
}
