namespace MotorDoctor.Core.Entities;

public class SettingDetail : BaseEntity
{
    public string Value { get; set; } = null!;
    public int SettingId { get; set; }
    public Setting Setting { get; set; } = null!;
    public Language Language { get; set; } = null!;
    public int LanguageId { get; set; }
}
