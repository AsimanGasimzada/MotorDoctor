namespace MotorDoctor.Core.Entities;

public class OrderItem : BaseAuditableEntity
{
    public decimal StaticPrice { get; set; }
    public decimal StaticDiscount { get; set; }
    public int Count { get; set; }
    public int ProductSizeId { get; set; }
    public ProductSize ProductSize { get; set; } = null!;
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
}
