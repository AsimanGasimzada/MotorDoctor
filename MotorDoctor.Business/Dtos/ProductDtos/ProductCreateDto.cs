using Microsoft.AspNetCore.Http;
using MotorDoctor.Core.Entities;

namespace MotorDoctor.Business.Dtos;

public class ProductCreateDto : IDto
{
    public string Code { get; set; } = null!;
    public List<CategoryForProductGetDto>? Categories { get; set; } = [];
    public int CategoryId { get; set; }
    public List<BrandForProductGetDto>? Brands { get; set; } = [];
    public int BrandId { get; set; }
    public IFormFile MainImage { get; set; }=null!;
    public List<IFormFile> Images { get; set; } = [];
    public List<ProductSizeCreateDto> ProductSizes { get; set; } = [];
    public List<ProductDetailCreateDto> ProductDetails { get; set; } = [];
}
