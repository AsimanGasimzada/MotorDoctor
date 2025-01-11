namespace MotorDoctor.Business.Dtos;

public class OrderItemGetDto : IDto
{
    public int Id { get; set; }
    public decimal StaticPrice { get; set; }
    public decimal StaticDiscount { get; set; }
    public int ProductSizeId { get; set; }
    public ProductSizeRelationDto ProductSize { get; set; } = null!;
    public int Count { get; set; }
}
