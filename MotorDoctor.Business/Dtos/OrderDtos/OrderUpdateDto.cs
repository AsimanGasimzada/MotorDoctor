namespace MotorDoctor.Business.Dtos;

public class OrderUpdateDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<OrderItemUpdateDto> OrderItems { get; set; } = [];
}