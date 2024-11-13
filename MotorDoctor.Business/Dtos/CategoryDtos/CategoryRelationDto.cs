namespace MotorDoctor.Business.Dtos;

public class CategoryRelationDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public int ParentId { get; set; }
}