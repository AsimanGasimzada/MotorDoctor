namespace MotorDoctor.Core.Entities;

public class Product : BaseAuditableEntity
{
    public int SalesCount { get; set; } = 0;
    public string Slug { get; set; } = null!;
    public string KeyWords { get; set; } = null!;
    public string? Specifications { get; set; }
    public string? Recommendation { get; set; }
    public string Code { get; set; } = null!;
    public Brand Brand { get; set; } = null!;
    public int BrandId { get; set; }
    public Density? Density { get; set; }
    public int? DensityId { get; set; }
    //public Category Category { get; set; } = null!;
    //public int CategoryId { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = [];
    public ICollection<ProductDetail> ProductDetails { get; set; } = [];
    public ICollection<ProductSize> ProductSizes { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<ProductCategory> ProductCategories { get; set; } = [];

}
