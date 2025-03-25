namespace MotorDoctor.Business.Dtos;

public class ShopFilterDto : IDto
{
    public PaginateDto<ProductGetDto> Products { get; set; } = new();
    public ProductFilterDto? ProductFilterDto { get; set; } = new();
    public List<ParentCategoryForFilterDto> Categories { get; set; } = new();
    public List<BrandRelationDto> Brands { get; set; } = new();
    public List<DensityRelationDto> Densities { get; set; } = new();
    public List<AdvertisementGetDto> Advertisements { get; set; } = new();

}
