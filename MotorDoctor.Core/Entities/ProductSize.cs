namespace MotorDoctor.Core.Entities;

public class ProductSize : BaseAuditableEntity
{
    public string? Size { get; set; } 
    public decimal Price { get; set; }
    public int Count { get; set; }
    public decimal Discount { get; set; } = 0;
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
}