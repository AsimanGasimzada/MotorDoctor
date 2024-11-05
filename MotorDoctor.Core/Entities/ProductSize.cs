namespace MotorDoctor.Core.Entities;

public class ProductSize : BaseAuditableEntity
{
    public string Size { get; set; } = null!;
    public decimal Price { get; set; }
    public int Count { get; set; }
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
}
