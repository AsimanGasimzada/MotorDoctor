namespace MotorDoctor.Business.Dtos;

public class SettingCreateDto : IDto
{
    public string Key { get; set; } = null!;
    public List<SettingDetailCreateDto> SettingDetails { get; set; } = [];
}
