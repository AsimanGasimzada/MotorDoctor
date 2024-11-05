namespace MotorDoctor.Business.Dtos;

public class BranchDetailUpdateDto : IDto
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Name { get; set; } = null!;
    public string Location { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string WorkHours { get; set; } = null!;
}
