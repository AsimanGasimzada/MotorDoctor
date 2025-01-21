namespace MotorDoctor.Business.Dtos;

public class ProductGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int SalesCount { get; set; }
    public int ViewCount { get; set; }
    public List<CategoryRelationDto> Categories { get; set; } = [];
    public BrandRelationDto Brand { get; set; } = null!;
    public string MainImagePath { get; set; } = null!;
    public List<string> ImagePaths { get; set; } = [];

    public List<ProductSizeGetDto> ProductSizes { get; set; } = [];
    public string Slug { get; set; } = null!;


}
