namespace MotorDoctor.Core.Entities;

public class Product : BaseAuditableEntity
{
    public int SalesCount { get; set; } = 0;
    public string Code { get; set; } = null!;
    public Brand Brand { get; set; } = null!;
    public int BrandId { get; set; }
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = [];
    public ICollection<ProductDetail> ProductDetails { get; set; } = [];
    public ICollection<ProductSize> ProductSizes { get; set; } = [];

}
