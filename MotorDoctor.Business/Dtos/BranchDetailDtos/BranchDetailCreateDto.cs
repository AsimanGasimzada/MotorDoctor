namespace MotorDoctor.Business.Dtos;

public class BranchDetailCreateDto : IDto
{
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
    public int LanguageId { get; set; }
}
