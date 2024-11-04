namespace MotorDoctor.Core.Entities;

public class BrandDetail : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public Language Language { get; set; } = null!;
    public int LanguageId { get; set; }
}