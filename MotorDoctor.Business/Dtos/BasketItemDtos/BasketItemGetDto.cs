namespace MotorDoctor.Business.Dtos;

public class BasketItemGetDto : IDto
{
    public int ProductSizeId { get; set; }
    public ProductSizeRelationDto ProductSize { get; set; } = null!;
    public int Count { get; set; }
}


