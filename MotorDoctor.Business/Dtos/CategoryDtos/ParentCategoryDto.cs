namespace MotorDoctor.Business.Dtos;

public class ParentCategoryDto : IDto
{
    public string Name { get; set; }=null!;
    public string ImagePath { get; set; }=null!;
    public int ProductCount { get; set; }
}