namespace MotorDoctor.Core.Entities;

public class AttendanceDetail : BaseEntity
{

    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int LanguageId { get; set; }
    public Language Language { get; set; } = null!;
    public int AttedanceId { get; set; }
    public Attendance Attedance { get; set; } = null!;
}