namespace MotorDoctor.Core.Entities;

public class StatusDetail : BaseEntity
{
    public string Name { get; set; } = null!;

    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;

    public Status Status { get; set; } = null!;
    public int StatusId { get; set; }
}