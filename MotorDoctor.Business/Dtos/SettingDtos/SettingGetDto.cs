namespace MotorDoctor.Business.Dtos;

public class SettingGetDto : IDto
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}