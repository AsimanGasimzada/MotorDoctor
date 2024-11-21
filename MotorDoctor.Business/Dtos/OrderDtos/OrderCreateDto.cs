namespace MotorDoctor.Business.Dtos;

public class OrderCreateDto : IDto
{
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<OrderItemCreateDto>? OrderItems { get; set; } = [];
}
