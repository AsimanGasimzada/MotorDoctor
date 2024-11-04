namespace MotorDoctor.Business.Dtos;

public class ProductGetDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int CategoryId { get; set; } // configure dto
    public int BrandId { get; set; } // configure dto
    public string MainImagePath { get; set; } = null!;
    public List<string> ImagePaths { get; set; } = [];

    public List<ProductSizeGetDto> ProductSizes { get; set; } = [];

}
