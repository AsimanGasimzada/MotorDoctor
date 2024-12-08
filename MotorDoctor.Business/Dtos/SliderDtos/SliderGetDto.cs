namespace MotorDoctor.Business.Dtos;

public class SliderGetDto : IDto
{
    public int Id { get; set; }
    public string ImagePath { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ButtonTitle { get; set; } = null!;
    public string? ButtonPath { get; set; }
}