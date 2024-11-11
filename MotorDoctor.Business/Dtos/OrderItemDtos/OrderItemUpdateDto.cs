namespace MotorDoctor.Business.Dtos;

public class OrderItemUpdateDto : IDto
{
    public int Id { get; set; }
    public ProductSizeRelationDto ProductSize { get; set; } = null!;
    public int Count { get; set; }
}
