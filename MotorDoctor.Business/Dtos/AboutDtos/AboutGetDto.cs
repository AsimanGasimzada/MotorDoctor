namespace MotorDoctor.Business.Dtos;

public class AboutGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public int OrderNo { get; set; }
}
