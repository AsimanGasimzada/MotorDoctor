namespace MotorDoctor.Business.Dtos;

public class SliderDetailCreateDto : IDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ButtonTitle { get; set; } = null!;
    public int LanguageId { get; set; }
}

