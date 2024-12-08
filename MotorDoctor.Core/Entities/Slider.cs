namespace MotorDoctor.Core.Entities;

public class Slider : BaseAuditableEntity
{
    public string ImagePath { get; set; } = null!;
    public string? ButtonPath { get; set; }
    public ICollection<SliderDetail> SliderDetails { get; set; } = [];
}
