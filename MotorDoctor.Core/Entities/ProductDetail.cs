namespace MotorDoctor.Core.Entities;

public class ProductDetail : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public int ProductId { get; set; }
    public Language Language { get; set; } = null!;
    public int LanguageId { get; set; }
}