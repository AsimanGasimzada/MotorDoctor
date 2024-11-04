namespace MotorDoctor.Core.Entities;

public class Brand : BaseEntity
{
    public string ImagePath { get; set; } = null!;
    public ICollection<BrandDetail> BrandDetails { get; set; } = [];
}
