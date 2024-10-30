namespace MotorDoctor.Business.Dtos;

public class SettingUpdateDto : IDto
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public List<SettingDetailUpdateDto> SettingDetails { get; set; } = [];
}
