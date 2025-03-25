namespace MotorDoctor.Business.Dtos;

public class DensityGetDto : IDto
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;
}
