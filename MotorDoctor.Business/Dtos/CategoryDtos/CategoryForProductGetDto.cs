namespace MotorDoctor.Business.Dtos;

public class CategoryForProductGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}