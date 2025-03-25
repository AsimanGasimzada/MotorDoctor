namespace MotorDoctor.Business.Dtos;

public class BestSellerProductGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Code { get; set; } = null!;
    public CategoryRelationDto Category { get; set; } = null!;
    public CategoryRelationDto ParentCategory { get; set; } = null!;
    public BrandRelationDto Brand { get; set; } = null!;
    public DensityRelationDto? Density { get; set; }
    public string MainImagePath { get; set; } = null!;
    public List<string> ImagePaths { get; set; } = [];
    public List<ProductSizeGetDto> ProductSizes { get; set; } = [];
    public string Slug { get; set; } = null!;
}