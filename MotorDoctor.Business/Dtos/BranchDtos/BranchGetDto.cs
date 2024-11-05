namespace MotorDoctor.Business.Dtos;

public class BranchGetDto : IDto
{
    public int Id { get; set; }
    public string? ImagePath { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
}

