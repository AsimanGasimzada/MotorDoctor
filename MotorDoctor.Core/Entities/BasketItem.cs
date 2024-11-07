namespace MotorDoctor.Core.Entities;

public class BasketItem : BaseEntity
{
    public ProductSize ProductSize { get; set; } = null!;
    public int ProductSizeId { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public int Count { get; set; }

}
