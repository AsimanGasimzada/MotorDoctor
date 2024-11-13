namespace MotorDoctor.Business.Dtos;

public class WishlistItemGetDto:IDto
{
    public int ProductSizeId { get; set; }
    public ProductSizeRelationDto ProductSize { get; set; } = null!;
}
