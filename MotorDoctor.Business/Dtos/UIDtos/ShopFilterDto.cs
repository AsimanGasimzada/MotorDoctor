namespace MotorDoctor.Business.Dtos;

public class ShopFilterDto : IDto
{
    public PaginateDto<ProductGetDto> Products { get; set; } = new();
    public ProductFilterDto? ProductFilterDto { get; set; } = new();
    public List<CategoryRelationDto> Categories { get; set; } = new();
    public List<BrandRelationDto> Brands { get; set; } = new();

}
