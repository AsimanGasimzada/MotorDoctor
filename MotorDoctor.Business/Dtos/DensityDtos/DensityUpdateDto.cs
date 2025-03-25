namespace MotorDoctor.Business.Dtos;

public class DensityUpdateDto : IDto
{
    public int Id { get; set; }
    public string Value { get; set; } = null!;
}
