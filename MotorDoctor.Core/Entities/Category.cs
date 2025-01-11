namespace MotorDoctor.Core.Entities;

public class Category : BaseAuditableEntity
{
    public string? ImagePath { get; set; }
    public int? ParentId { get; set; }
    public Category? Parent { get; set; }
    public ICollection<CategoryDetail> CategoryDetails { get; set; } = [];
    public ICollection<Category> Children { get; set; } = [];
    public ICollection<ProductCategory> ProductCategories { get; set; } = [];
}
