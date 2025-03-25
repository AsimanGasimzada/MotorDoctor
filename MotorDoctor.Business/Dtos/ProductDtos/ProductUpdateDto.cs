using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class ProductUpdateDto : IDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public List<CategoryRelationDto>? Categories { get; set; } = [];
    public List<int> CategoryIds { get; set; } = [];
    public List<BrandRelationDto>? Brands { get; set; } = [];
    public List<DensityRelationDto>? Densities { get; set; } = [];
    public int BrandId { get; set; }
    public int? DensityId { get; set; }
    public string? MainImagePath { get; set; }
    public IFormFile? MainImage { get; set; }
    public List<IFormFile> Images { get; set; } = [];
    public List<string> ImagePaths { get; set; } = [];
    public List<int> ImageIds { get; set; } = [];
    public string Slug { get; set; } = null!;
    public string KeyWords { get; set; } = null!;
    public List<ProductSizeUpdateDto> ProductSizes { get; set; } = [];
    public List<ProductDetailUpdateDto> ProductDetails { get; set; } = [];
    public string? Specifications { get; set; }
    public string? Recommendation { get; set; }
}
