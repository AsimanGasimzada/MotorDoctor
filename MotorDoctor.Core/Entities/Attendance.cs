namespace MotorDoctor.Core.Entities;

public class Attendance : BaseEntity
{
    public string ImagePath { get; set; } = null!;
    public ICollection<AttendanceDetail> AttendanceDetails { get; set; } = [];
}
