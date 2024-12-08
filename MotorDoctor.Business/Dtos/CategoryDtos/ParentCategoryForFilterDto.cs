namespace MotorDoctor.Business.Dtos;

public class ParentCategoryForFilterDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<CategoryRelationDto> Children { get; set; } = [];
}