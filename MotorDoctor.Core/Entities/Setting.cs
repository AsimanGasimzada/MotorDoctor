namespace MotorDoctor.Core.Entities;

public class Setting : BaseEntity
{
    public string Key { get; set; } = null!;
    public ICollection<SettingDetail> SettingDetails { get; set; } = [];
}
