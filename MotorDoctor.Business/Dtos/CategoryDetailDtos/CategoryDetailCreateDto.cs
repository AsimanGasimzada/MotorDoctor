namespace MotorDoctor.Business.Dtos;

public class CategoryDetailCreateDto:IDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int LanguageId { get; set; }
}
