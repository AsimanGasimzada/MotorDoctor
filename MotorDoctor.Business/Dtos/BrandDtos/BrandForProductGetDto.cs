namespace MotorDoctor.Business.Dtos;

public class BrandForProductGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}