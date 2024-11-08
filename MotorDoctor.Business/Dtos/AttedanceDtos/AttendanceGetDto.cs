namespace MotorDoctor.Business.Dtos;

public class AttendanceGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string ImagePath { get; set; }=null!;
}
