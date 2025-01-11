namespace MotorDoctor.Core.Entities;

public class Advertisement : BaseAuditableEntity
{
    public string ImagePath { get; set; } = null!;
    public string Url { get; set; } = null!;
}
