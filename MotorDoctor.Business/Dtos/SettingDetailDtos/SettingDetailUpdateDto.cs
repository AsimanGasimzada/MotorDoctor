namespace MotorDoctor.Business.Dtos;

public class SettingDetailUpdateDto : IDto
{
    public int LanguageId { get; set; }
    public string Value { get; set; } = null!;
}
