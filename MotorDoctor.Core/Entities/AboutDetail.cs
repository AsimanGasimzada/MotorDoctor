namespace MotorDoctor.Core.Entities;

public class AboutDetail : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public Language Language { get; set; } = null!;
    public int LanguageId { get; set; }
    public About About { get; set; } = null!;
    public int AboutId { get; set; }
}
