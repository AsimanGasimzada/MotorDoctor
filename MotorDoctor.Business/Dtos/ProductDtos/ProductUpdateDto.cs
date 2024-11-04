using Microsoft.AspNetCore.Http;

namespace MotorDoctor.Business.Dtos;

public class ProductUpdateDto : IDto
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public List<CategoryForProductGetDto>? Categories { get; set; } = [];
    public int CategoryId { get; set; }
    public List<BrandForProductGetDto>? Brands { get; set; } = [];
    public int BrandId { get; set; }

    public string? MainImagePath { get; set; }
    public IFormFile? MainImage { get; set; }
    public List<IFormFile> Images { get; set; } = [];
    public List<string> ImagePaths { get; set; } = [];
    public List<int> ImageIds { get; set; } = [];


    public List<ProductSizeUpdateDto> ProductSizes { get; set; } = [];
    public List<ProductDetailCreateDto> ProductDetails { get; set; } = [];
}
