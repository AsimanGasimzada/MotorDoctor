namespace MotorDoctor.Business.Dtos;

public class AttedanceDetailCreateDto : IDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int LanguageId { get; set; }
}
