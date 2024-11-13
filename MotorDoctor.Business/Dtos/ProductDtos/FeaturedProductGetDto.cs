namespace MotorDoctor.Business.Dtos;

public class FeaturedProductGetDto : IDto
{
    public List<ProductGetDto> Products { get; set; } = [];
    public List<CategoryRelationDto> Categories { get; set; } = [];

}