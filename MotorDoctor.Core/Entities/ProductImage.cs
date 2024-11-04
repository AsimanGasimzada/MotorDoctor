namespace MotorDoctor.Core.Entities;

public class ProductImage : BaseEntity
{
    public string Path { get; set; } = null!;
    public bool IsMain { get; set; } = false;
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
}
