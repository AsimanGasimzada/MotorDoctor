namespace MotorDoctor.Business.Dtos;

public class TopUserDto
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public int OrderCount { get; set; }
    public decimal TotalSpent { get; set; }
    public string? PhoneNumber { get; set; }=null!;
}
