namespace MotorDoctor.Business.Dtos;

public class SettingDetailCreateDto : IDto
{
    public int LanguageId { get; set; }
    public string Value { get; set; } = null!;
}
