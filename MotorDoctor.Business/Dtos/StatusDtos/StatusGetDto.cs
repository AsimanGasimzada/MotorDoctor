namespace MotorDoctor.Business.Dtos;

public class StatusGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
