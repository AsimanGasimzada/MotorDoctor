namespace MotorDoctor.Core.Entities;

public class Advertisement : BaseEntity
{
    public string ImagePath { get; set; } = null!;
    public string Url { get; set; } = null!;
}
