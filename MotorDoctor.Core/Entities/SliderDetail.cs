namespace MotorDoctor.Core.Entities;

public class SliderDetail : BaseEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; } 
    public string ButtonTitle { get; set; } = null!;
    public Slider Slider { get; set; } = null!;
    public int SliderId { get; set; }
    public Language Language { get; set; } = null!;
    public int LanguageId { get; set; }
}
