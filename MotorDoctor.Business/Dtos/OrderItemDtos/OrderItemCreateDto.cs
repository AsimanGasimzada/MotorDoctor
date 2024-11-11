namespace MotorDoctor.Business.Dtos;

public class OrderItemCreateDto : IDto
{
    public int ProductSizeId { get; set; }
    public ProductSizeRelationDto ProductSize { get; set; } = null!;
    public int Count { get; set; }
}
